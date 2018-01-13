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

    internal class TheragathaProvider: IProvider
    {
        //working dir
        //C:\development\Pali-Canon\Api\PaliCanon.Loader\source\ati_website\html\tipitaka\kn\thag\thag.01.00x.than.html
        
        private const string SITEBASE = @"source\ati_website\html\tipitaka\kn\thag";
        //private const string SITEBASE = @"source\ati_website\debug";
        
        private ChapterRepository chapterRepository;
      
        public event EventHandler<NotifyEventArgs> OnNotify;

        public TheragathaProvider(ChapterRepository chapterRepository)
        {
            this.chapterRepository = chapterRepository;
        }


        public void Load()
        {
            
            HtmlDocument index = new HtmlDocument(); 
            index.Load(Path.Combine(SITEBASE, "thag.01.00x.than.html").ToApplicationPath());  
            
            var chapterNodes = index.DocumentNode.SelectNodes("//div[contains(@class, 'chapter')]");
            foreach(var chapterNode in chapterNodes){
                GetChapter(chapterNode);
            }
        }

        public void GetChapter(HtmlNode chapterNode)
        {
               var titleNode = chapterNode.Descendants("a").FirstOrDefault();
               var textNode = chapterNode.SelectNodes("div[contains(@class, 'freeverse')]").FirstOrDefault();

                if(titleNode == null || textNode == null) return;

                Regex chapterParser = new Regex(@"([\S\s]*?)\([\S\s]*?([\d]+)\.([\d]+)\)");
                var match = chapterParser.Match(titleNode.InnerText);

                if(match.Groups.Count < 4) return;

                var chapterTitle = match.Groups[1].Value;
                Int32.TryParse(match.Groups[2].Value, out int chapterNumber);
                Int32.TryParse(match.Groups[3].Value, out int verseNumber);

                var chapter = new Chapter();
                chapter.Title = chapterTitle;
                chapter.Author = "Thanissaro";
                chapter.Nikaya = "Khuddaka";
                chapter.Book = "Theragatha";
                chapter.BookCode = "thag";
                chapter.ChapterNumber = chapterNumber;

                var verseToAdd = new Verse{ VerseNumber = verseNumber, Text = textNode.InnerText};
                chapter.Verses.Add(verseToAdd);
                chapterRepository.Insert(chapter);

                var message = $"loading thag {chapterNumber} {verseNumber}";
                if(OnNotify != null) OnNotify(this, new NotifyEventArgs(message));
        }
    }
}