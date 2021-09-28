using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using PaliCanon.Common.Extensions;
using PaliCanon.Contracts;
using PaliCanon.Contracts.Book;
using PaliCanon.Contracts.Chapter;
using PaliCanon.Model;

namespace PaliCanon.DataLoad.Provider
{
    public class DhammapadaProvider: IProvider
    {
        private const string Sitebase = @"source\ati_website\html\tipitaka\kn\dhp";
        private const string Nikaya = "Khuddaka";
        private const string Book = "Dhammapada";
        private const string BookCode = "dhp";

        private readonly IBookService _bookService;
        private readonly IChapterService _chapterService;
      
        public event EventHandler<NotifyEventArgs> OnNotify;

        public DhammapadaProvider(IBookService bookService, IChapterService chapterService)
        {
            _bookService = bookService;
            _chapterService = chapterService;
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
                
                //Acharya Buddharakkhita
                if(Regex.IsMatch(chapterHref, @"[\S\s]*\d[\S\s]budd[\S\s]*"))
                { 
                    var message = $"loading {chapterHref}";
                    OnNotify?.Invoke(this, new NotifyEventArgs(message));

                    HtmlDocument chapterPage = new HtmlDocument(); 
                    chapterPage.Load(chapterHref);
                    AddChapter(chapterPage, chapterNumber);

                    chapterNumber++;
                }
            }
        }
        
        private void AddChapter(HtmlDocument document, int chapterNumber){
               
            
            var titleNode = document.DocumentNode.SelectNodes("//title").FirstOrDefault();

            var translatedBy = document.DocumentNode.SelectSingleNode("//div[contains(@id, 'H_docAuthor')]");
            var author = translatedBy.InnerText;


            if (titleNode != null)
            {
                var chapter = new Chapter
                {
                    Title = titleNode.InnerText,
                    Author = author,
                    Nikaya = Nikaya,
                    Book = Book,
                    BookCode = BookCode,
                    ChapterNumber = chapterNumber
                };

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

                chapter.Citation = $"{chapter.Title} ({BookCode} {chapterNumber}), translated from the Pali by {author}. Access to Insight (https://www.accesstoinsight.org/)";
                _chapterService.Insert(chapter);
            }
        }

        private void AddBook()
        {
            Book book = new Book { Code = BookCode, Description = Book, Nikaya = Nikaya};
            _bookService.Insert(book);
        }
    }
}