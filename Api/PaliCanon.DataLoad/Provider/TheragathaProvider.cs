using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using PaliCanon.Common.Enums;
using PaliCanon.Common.Extensions;
using PaliCanon.Contracts;
using PaliCanon.Contracts.Chapter;
using PaliCanon.Model;
using static System.Int32;

namespace PaliCanon.DataLoad.Provider
{
    public class TheragathaProvider: IProvider
    {
        private readonly List<SourceFile> _sources;

        //working dir
        //C:\development\Pali-Canon\Api\PaliCanon.Loader\source\ati_website\html\tipitaka\kn\thag\thag.01.00x.than.html
    
        private const string Sitebase = @"source\ati_website\html\tipitaka\kn\thag";
        //private readonly string verseNumberRegex = @"([\S\s]*?)\([\S\s]*?([\d]+)\.([\d]+)\)";
        //private const string SITEBASE = @"source\ati_website\debug";
        
        private readonly IChapterService _chapterService;
      
        public event EventHandler<NotifyEventArgs> OnNotify;

        public TheragathaProvider(IChapterService chapterService)
        {
            _chapterService = chapterService;
            _sources = new List<SourceFile>
            {
                new SourceFile {Type = ChapterType.ThagSingleVerse, Location = "thag.01.00x.than.html"}
            };

            DirectoryInfo dir = new DirectoryInfo(Sitebase.ToApplicationPath());
            foreach(var file in dir.GetFiles())
            {
                Regex fileMatch = new Regex(@"thag\.(\d+\.\d+)\.than.html");
                if(fileMatch.IsMatch(file.Name) && file.Name != "thag.01.00x.than.html")
                    _sources.Add(new SourceFile { Type = ChapterType.ThagMultipleVerse, Location = file.Name});
            }

            //_sources.Add(new SourceFile { Type = ChapterType.ThagMultipleVerse, Location = "thag.02.13.than.html"});
            //_sources.Add(new SourceFile { Type = ChapterType.ThagMultipleVerse, Location = "thag.02.24.than.html"});
        }

        public void Load()
        {
            
            HtmlDocument index = new HtmlDocument(); 
             
            foreach(var source in _sources)
            {
                index.Load(Path.Combine(Sitebase, source.Location).ToApplicationPath());  

                try
                {
                    if(source.Type == ChapterType.ThagSingleVerse)
                    {
                        GetChapterSingleVerse(index);
                    }
                    else if(source.Type == ChapterType.ThagMultipleVerse)
                    {
                        GetChapterMultipleVerse(index, source.Location);
                    }
                }
                catch(Exception ex)
                {
                    var message = $"ERROR {source.Location} {ex.Message}";
                    OnNotify?.Invoke(this, new NotifyEventArgs(message, true));
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
                    TryParse(match.Groups[1].Value, out firstVerseNumber);
                    TryParse(match.Groups[2].Value, out lastVerseNumber);
                }

                Regex chapterMatch = new Regex(@"Thag ([\d]+)\.([\d]+)");
                match = chapterMatch.Match(node.InnerText);    
                if(match.Success)
                {
                    TryParse(match.Groups[1].Value, out chapterNumber);
                }
            }

            var textNode = index.DocumentNode.SelectNodes("//div[contains(@class, 'freeverse')]").FirstOrDefault();
            var titleNode = index.DocumentNode.SelectNodes("//div[contains(@id, 'H_docTitle')]").FirstOrDefault();
            

            if(chapterNumber != 0 && firstVerseNumber > 0 && textNode != null && titleNode != null)
            {
                var chapter = InitChapter();
                chapter.Title = titleNode.InnerText.Trim();
                chapter.ChapterNumber = chapterNumber;

                var parsedVerses = ParseFreeVerse(textNode.InnerText);

                if(parsedVerses.Count == lastVerseNumber - firstVerseNumber + 1)
                {
                    //.. successfully parsed free verse into correct number of verses
                    var thisVerse = firstVerseNumber;
                    foreach(var parsedVerse in parsedVerses)
                    {
                         var verseToAdd = new Verse
                        { 
                            VerseNumber = thisVerse,
                            Text = parsedVerse
                        };
                        chapter.Verses.Add(verseToAdd);
                        thisVerse++;
                    }

                    var message = $"loading thag {chapterNumber} {firstVerseNumber}";
                    OnNotify?.Invoke(this, new NotifyEventArgs(message));


                    _chapterService.Insert(chapter);
                }
                else
                {
                    var message = $"PARSE ERROR. Cannot parse free text verse {location}";
                    OnNotify?.Invoke(this, new NotifyEventArgs(message));

                    // //.. else insert verse into range

                    // var verseToAdd = new Verse
                    // { 
                    //     VerseNumber = firstVerseNumber, 
                    //     VerseNumberLast = lastVerseNumber,
                    //     Text = textNode.InnerText
                    // };
                    // chapter.Verses.Add(verseToAdd);
                
                }
                  
            }
            else
            {
                var message = $"INCOMPLETE INFORMATION. Cannot parse thag {location}";
                OnNotify?.Invoke(this, new NotifyEventArgs(message, true));
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
                TryParse(match.Groups[2].Value, out int chapterNumber);
                TryParse(match.Groups[3].Value, out int verseNumber);

                var chapter = InitChapter();
                chapter.Title = chapterTitle;
                chapter.ChapterNumber = chapterNumber;

                var verseToAdd = new Verse{ VerseNumber = verseNumber, Text = textNode.InnerText};
                chapter.Verses.Add(verseToAdd);
                _chapterService.Insert(chapter);

                var message = $"loading thag {chapterNumber} {verseNumber}";
                OnNotify?.Invoke(this, new NotifyEventArgs(message));
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

        private List<String> ParseFreeVerse(string freeVerse)
        {
            Regex parser = new Regex(@"\n{2,}");
            var returnValue = parser.Split(freeVerse).ToList();
            return returnValue;
        }
    }

  
}