using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using PaliCanon.Common.Extensions;
using PaliCanon.Contracts;
using PaliCanon.Model;

namespace PaliCanon.DataLoad.Provider
{
    public class DhammapadaProvider: IProvider
    {
        private const string Sitebase = @"source\ati_website\html\tipitaka\kn\dhp";

        private readonly IBookRepository _bookRepository;
        private readonly IChapterRepository _chapterRepository;
      
        public event EventHandler<NotifyEventArgs> OnNotify;

        public DhammapadaProvider(IBookRepository bookRepository, IChapterRepository chapterRepository)
        {
            _bookRepository = bookRepository;
            _chapterRepository = chapterRepository;
        }

        public void Load()
        {
            AddBook();
            HtmlDocument index = new HtmlDocument(); 
            index.Load(Path.Combine(Sitebase, "index.html").ToApplicationPath());  
            
            var links = index.DocumentNode.SelectNodes("//span[contains(@class, 'sutta_trans')]").Descendants("a");

            int chapterNumber = 1;
            foreach(var link in links)
            {
                var chapterHref = Path.Combine(Sitebase, link.Attributes["href"].Value).ToApplicationPath();
                var author = link.InnerText;

                //Acharya Buddharakkhita
                if(Regex.IsMatch(chapterHref, @"[\S\s]*\d[\S\s]budd[\S\s]*"))
                { 
                    var message = $"loading {chapterHref}";
                    OnNotify?.Invoke(this, new NotifyEventArgs(message));

                    HtmlDocument chapterPage = new HtmlDocument(); 
                    chapterPage.Load(chapterHref);
                    GetChapter(chapterPage, author, chapterNumber);

                    chapterNumber++;
                }
            }
        }
        
        private void GetChapter(HtmlDocument document, string author, int chapterNumber){
               
            var titleNode = document.DocumentNode.SelectNodes("//title").FirstOrDefault();  
            
            if(titleNode != null)
            {
                var chapter = new Chapter();
                chapter.Title = titleNode.InnerText;
                chapter.Author = author;
                chapter.Nikaya = "Khuddaka";
                chapter.BookTitle = "Dhammapada";
                chapter.BookCode = "dhp";
                chapter.ChapterNumber = chapterNumber;

                var verses = document.DocumentNode.SelectNodes("//div[contains(@class, 'verse')]").Descendants("p");
                foreach(var verse in verses)
                {
                    var verseNumberString = verse.Descendants("b").FirstOrDefault()?.InnerText;

                    var verseNumbers = Regex.Matches(verseNumberString ?? string.Empty, @"\d+")
                                                                                .Where(x => int.TryParse(x.Value, out int dummy))
                                                                                .Select(x => int.Parse(x.Value))
                                                                                .ToList();
                    if(verseNumbers.Any())
                    {
                        var verseNodes = verse.ChildNodes.Skip(1).Select(x => x.InnerText.Clean()).ToArray();
                        var verseText = string.Join("", verseNodes);
                        var verseToAdd = new Verse{ VerseNumber = verseNumbers.First(), Text = verseText};

                        if(verseNumbers.Count > 1) verseToAdd.VerseNumberLast = verseNumbers.Last();
 
                        chapter.Verses.Add(verseToAdd);
                    }
                }
                _chapterRepository.Insert(chapter);
            }
        }

        private void AddBook()
        {
            Book book = new Book { Code = "dhp", Description = "dhammapada" };
            _bookRepository.Insert(book);
        }
    }
}