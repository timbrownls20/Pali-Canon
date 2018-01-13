using System.Text.RegularExpressions;
using System.Linq;
using HtmlAgilityPack;
using System.IO;
using MongoDB.Driver;
using System;
using PaliCanon.Common.Repository;
using PaliCanon.Common.Extensions;
using PaliCanon.Common.Model;
using System.Collections.Generic;

namespace PaliCanon.Loader.Provider
{

    internal class TheragathaProvider: IProvider
    {
        private readonly List<SourceFile> _sources;


        //working dir
        //C:\development\Pali-Canon\Api\PaliCanon.Loader\source\ati_website\html\tipitaka\kn\thag\thag.01.00x.than.html
    

        private const string SITEBASE = @"source\ati_website\html\tipitaka\kn\thag";
        //private readonly string verseNumberRegex = @"([\S\s]*?)\([\S\s]*?([\d]+)\.([\d]+)\)";
        //private const string SITEBASE = @"source\ati_website\debug";
        
        private ChapterRepository _chapterRepository;
      
        public event EventHandler<NotifyEventArgs> OnNotify;

        public TheragathaProvider(ChapterRepository chapterRepository)
        {
            _chapterRepository = chapterRepository;
            _sources = new List<SourceFile>();
            _sources.Add(new SourceFile { Type = ChapterType.ThagSingleVerse, Location = "thag.01.00x.than.html"});
            _sources.Add(new SourceFile { Type = ChapterType.ThagMultipleVerse, Location = "thag.02.13.than.html"});

        }


        public void Load()
        {
            
            HtmlDocument index = new HtmlDocument(); 
             
            foreach(var source in _sources)
            {
                index.Load(Path.Combine(SITEBASE, source.Location).ToApplicationPath());  

                if(source.Type == ChapterType.ThagSingleVerse)
                {
                    GetChapterSingleVerse(index);
                }
                else if(source.Type == ChapterType.ThagMultipleVerse)
                {
                    GetChapterMultipleVerse(index, source.Location);
                }
              

                
            }
            
          
        }

        public void GetChapterMultipleVerse(HtmlDocument index, string location)
        {
            int firstVerseNumber = 0, lastVerseNumber = 0, chapterNumber = 0;

            foreach (HtmlNode node in index.DocumentNode.SelectNodes("//text()[normalize-space(.) != '']"))
            {
                Regex verseMatch = new Regex(@"Thag ([\d]+)-([\d]+)");
                var match = verseMatch.Match(node.InnerText);     
                if(match.Success)
                {
                    Int32.TryParse(match.Groups[1].Value, out firstVerseNumber);
                    Int32.TryParse(match.Groups[2].Value, out lastVerseNumber);
                }

                Regex chapterMatch = new Regex(@"Thag ([\d]+)\.([\d]+)");
                match = chapterMatch.Match(node.InnerText);    
                if(match.Success)
                {
                    Int32.TryParse(match.Groups[1].Value, out chapterNumber);
                }
            }

            var textNode = index.DocumentNode.SelectNodes("//div[contains(@class, 'freeverse')]").FirstOrDefault();
            var titleNode = index.DocumentNode.SelectNodes("//div[contains(@id, 'H_docTitle')]").FirstOrDefault();
            

            if(chapterNumber != 0 && firstVerseNumber > 0 && textNode != null && titleNode != null)
            {
                    var chapter = InitChapter();
                    chapter.Title = titleNode.InnerText.Trim();
                    chapter.ChapterNumber = chapterNumber;

                    var verseToAdd = new Verse
                    { 
                        VerseNumber = firstVerseNumber, 
                        VerseNumberLast = lastVerseNumber,
                        Text = textNode.InnerText
                    };
                    chapter.Verses.Add(verseToAdd);
                    _chapterRepository.Insert(chapter);

                    var message = $"loading thag {chapterNumber} {firstVerseNumber}";
                    if(OnNotify != null) OnNotify(this, new NotifyEventArgs(message));
            }
            else
            {
                var message = $"Cannot process thag {location}";
                if(OnNotify != null) OnNotify(this, new NotifyEventArgs(message, true));
            }
                     
                 
        }

        public void GetChapterSingleVerse(HtmlDocument index)
        {
            var chapterNodes = index.DocumentNode.SelectNodes("//div[contains(@class, 'chapter')]");
            foreach(var chapterNode in chapterNodes){

               var titleNode = chapterNode.Descendants("a").FirstOrDefault();
               var textNode = chapterNode.SelectNodes("div[contains(@class, 'freeverse')]").FirstOrDefault();

                if(titleNode == null || textNode == null) return;

                Regex chapterParser = new Regex(@"([\S\s]*?)\([\S\s]*?([\d]+)\.([\d]+)\)");
                var match = chapterParser.Match(titleNode.InnerText);

                if(match.Groups.Count < 4) return;

                var chapterTitle = match.Groups[1].Value;
                Int32.TryParse(match.Groups[2].Value, out int chapterNumber);
                Int32.TryParse(match.Groups[3].Value, out int verseNumber);

                var chapter = InitChapter();
                chapter.Title = chapterTitle;
                chapter.ChapterNumber = chapterNumber;

                var verseToAdd = new Verse{ VerseNumber = verseNumber, Text = textNode.InnerText};
                chapter.Verses.Add(verseToAdd);
                _chapterRepository.Insert(chapter);

                var message = $"loading thag {chapterNumber} {verseNumber}";
                if(OnNotify != null) OnNotify(this, new NotifyEventArgs(message));
            }
        }

        private Chapter InitChapter()
        {
            var chapter = new Chapter
            {
                Author = "Thanissaro",
                Nikaya = "Khuddaka",
                Book = "Theragatha",
                BookCode = "thag"
            };
    
            return chapter;
        }
    }

  
}