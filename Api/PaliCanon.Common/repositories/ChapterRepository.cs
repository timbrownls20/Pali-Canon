using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PaliCanon.Common.Model;
using System.Linq;

namespace PaliCanon.Common.Repository
{
    public class ChapterRepository: IChapterRepository
    {
        IMongoDatabase database;

        public ChapterRepository(IMongoDatabase database)
        {
            this.database = database;    
        }

        public void Insert(Chapter record)
        {
            var collection = database.GetCollection<Chapter>(nameof(Chapter));
            collection.InsertOne(record);
        }

        public List<Chapter> Get(string bookCode, int? chapterId, int? verse)
        { 
            var collection = database.GetCollection<Chapter>(nameof(Chapter));
            var query = collection.AsQueryable<Chapter>().Where(x => x.BookCode == bookCode);
            List<Chapter> chapters = new List<Chapter>();

            if(chapterId.HasValue)
            {
                var chapter = query.Where(x => x.ChapterNumber == chapterId).SingleOrDefault();
                if(chapter != null)
                {
                    if(verse.HasValue)
                    {
                        chapter.Verses.RemoveAll(x => x.VerseNumber != verse);
                    }
                    chapters.Add(chapter);
                }
            }
            else
            {
                chapters = query.ToList();
            }
            
            return chapters;
        }

        public Chapter Next(string bookCode, int chapterId, int verse)
        { 
            return GetNearestVerse(bookCode, verse + 1);
        }

        public Chapter First(string bookCode)
        { 
            return Get(bookCode, 1, 1).FirstOrDefault();
        }

        public Chapter Last(string bookCode)
        { 
            int chapterId = LastChapterId(bookCode);
            int verseId = LastVerseId(bookCode);
            return Get(bookCode, chapterId, verseId).FirstOrDefault();
        }

        public Chapter Quote(string bookCode)
        { 
            var lastVerse = LastVerseId(bookCode);

            if(lastVerse == 0) return BlankChapter();


            Random rnd = new Random();
           
            int randomVerse = rnd.Next(1, lastVerse);
            var randomChapter = GetNearestVerse(bookCode, randomVerse);

            return randomChapter;
        }

        private Chapter GetNearestVerse(string bookCode, int verse)
        {
            var collection = database.GetCollection<Chapter>(nameof(Chapter));
            var chapter = collection.AsQueryable<Chapter>().Where(x => x.BookCode == bookCode
                        && x.Verses.Any(y => y.VerseNumber >= verse))
                        .OrderBy(x => x.ChapterNumber)
                        .FirstOrDefault();

            if(chapter != null){
                 var selectedVerse = chapter.Verses
                                        .Where(x => x.VerseNumber >= verse)
                                        .OrderBy(x => x.VerseNumber)
                                        .FirstOrDefault();

                int selectedVerseId = selectedVerse?.VerseNumber ?? 0;
                chapter.Verses.RemoveAll(x => x.VerseNumber != selectedVerseId);
            }



            if(chapter == null || !chapter.Verses.Any()){
                chapter = Last(bookCode);
            }

            return chapter;
        }

        private int LastChapterId(string bookCode){
            
            var collection = database.GetCollection<Chapter>(nameof(Chapter));
            
            var maxChapter = collection.AsQueryable<Chapter>()
                .Where(x => x.BookCode == bookCode)
                .OrderByDescending(x => x.ChapterNumber)
                .Select(x => x.ChapterNumber)
                .FirstOrDefault();

            return maxChapter;
        }

        private int LastVerseId(string bookCode){
            
            var collection = database.GetCollection<Chapter>(nameof(Chapter));
            
            var verses = collection.AsQueryable<Chapter>()
                .Where(x => x.BookCode == bookCode)
                .OrderByDescending(x => x.ChapterNumber)
                .SelectMany(x => x.Verses).ToList();


            var maxVerse = verses.OrderByDescending(x => x.VerseNumber).FirstOrDefault();

            return (maxVerse != null) ? maxVerse.VerseNumber : 0;
        }

        private Chapter BlankChapter(){
            return new Chapter{ Verses = new List<Verse>{ new Verse{ Text = "No verse found" }}};
        }
    }
}