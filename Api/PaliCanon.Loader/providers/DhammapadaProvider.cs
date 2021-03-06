using System.Text.RegularExpressions;
using System.Linq;
using HtmlAgilityPack;
using System.IO;
using MongoDB.Driver;
using System;
using PaliCanon.Common.Repository;
using PaliCanon.Common.Extensions;
using PaliCanon.Common.Model;


namespace PaliCanon.Loader.Provider
{



    internal class DhammapadaProvider: IProvider
    {
        

        private const string SITEBASE = @"source\ati_website\html\tipitaka\kn\dhp";
        //private const string SITEBASE = @"source\ati_website\debug";
        
        private ChapterRepository chapterRepository;
      
        public event EventHandler<NotifyEventArgs> OnNotify;

        public DhammapadaProvider(ChapterRepository chapterRepository)
        {
            this.chapterRepository = chapterRepository;
        }


        public void Load()
        {
            
            HtmlDocument index = new HtmlDocument(); 
            index.Load(Path.Combine(SITEBASE, "index.html").ToApplicationPath());  
            
            var links = index.DocumentNode.SelectNodes("//span[contains(@class, 'sutta_trans')]").Descendants("a");

            int chapterNumber = 1;
            foreach(var link in links)
            {
                var chapterHref = Path.Combine(SITEBASE, link.Attributes["href"].Value).ToApplicationPath();
                var author = link.InnerText;

                //Acharya Buddharakkhita
                if(Regex.IsMatch(chapterHref, @"[\S\s]*\d[\S\s]budd[\S\s]*"))
                { 

                    var message = $"loading {chapterHref}";
                    if(OnNotify != null) OnNotify(this, new NotifyEventArgs(message));

                    HtmlDocument chapterPage = new HtmlDocument(); 
                    chapterPage.Load(chapterHref);
                    GetChapter(chapterPage, author, chapterNumber);

                    chapterNumber++;
                }
            }
        }

         public void GetChapter(HtmlDocument document, string author, int chapterNumber){
                
            
            var titleNode = document.DocumentNode.SelectNodes("//title").FirstOrDefault();  
            
            if(titleNode != null)
            {
                var chapter = new Chapter();
                chapter.Title = titleNode.InnerText;
                chapter.Author = author;
                chapter.Nikaya = "Khuddaka";
                chapter.Book = "Dhammapada";
                chapter.BookCode = "dhp";
                chapter.ChapterNumber = chapterNumber;

                var verses = document.DocumentNode.SelectNodes("//div[contains(@class, 'verse')]").Descendants("p");
                foreach(var verse in verses)
                {
                    var verseNumberString = verse.Descendants("b").FirstOrDefault().InnerText;

                    var verseNumbers = Regex.Matches(verseNumberString, @"\d+").Where(x => int.TryParse(x.Value, out int dummy))
                                                                                .Select(x => int.Parse(x.Value))
                                                                                .ToList();

                    //if (int.TryParse(Regex.Match(verseNumberString, @"\d+").Value, out var verseNumber))
                    if(verseNumbers.Any())
                    {
                        var verseNodes = verse.ChildNodes.Skip(1).Select(x => x.InnerText.Clean()).ToArray();
                        var verseText = string.Join("", verseNodes);
                        var verseToAdd = new Verse{ VerseNumber = verseNumbers.First(), Text = verseText};

                        if(verseNumbers.Count > 1) verseToAdd.VerseNumberLast = verseNumbers.Last();
 
                        chapter.Verses.Add(verseToAdd);
                    }
        
                }

                chapterRepository.Insert(chapter);
            }
        }
    }
}