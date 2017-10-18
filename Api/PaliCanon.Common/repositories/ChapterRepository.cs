using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PaliCanon.Common.Model;

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
    }
}