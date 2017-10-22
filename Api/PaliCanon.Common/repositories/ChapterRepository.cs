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
                if(verse.HasValue)
                {
                    chapter.Verses.RemoveAll(x => x.VerseNumber != verse);
                }
                chapters.Add(chapter);
            }
            else
            {
                chapters = query.ToList();
            }
            
            return chapters;
        }

        public Chapter Quote(string bookCode)
        { 
            Random rnd = new Random();
            var collection = database.GetCollection<Chapter>(nameof(Chapter));
            
            var maxChapter = collection.AsQueryable<Chapter>()
                .OrderByDescending(x => x.ChapterNumber)
                .Select(x => x.ChapterNumber)
                .FirstOrDefault();

            int randomChapterNumber = rnd.Next(1, maxChapter + 1);

            var randomChapter = collection.AsQueryable<Chapter>()
                .Where(x => x.ChapterNumber == randomChapterNumber)
                .FirstOrDefault();

            //.. this is now LINQ to Objects
            var maxVerse = randomChapter.Verses.Max(x => x.VerseNumber);
            var minVerse = randomChapter.Verses.Min(x => x.VerseNumber);
            int randomVerse = rnd.Next(minVerse, maxVerse + 1);
            randomChapter.Verses.RemoveAll(x => x.VerseNumber != randomVerse);


            return randomChapter;
        }
    }
}