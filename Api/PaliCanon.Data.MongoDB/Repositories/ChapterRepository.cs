using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PaliCanon.Contracts.Chapter;
using PaliCanon.Data.MongoDB.Entities;

namespace PaliCanon.Data.MongoDB.Repositories
{
    public class ChapterRepository: IChapterRepository<ChapterEntity, VerseEntity>
    {
        readonly IMongoDatabase _database;

        public ChapterRepository(Microsoft.Extensions.Configuration.IConfiguration config)
        {
            var mongo = new MongoDbContext(config);
            mongo.Drop();
            _database = mongo.Connect();
        }

        public void Insert(ChapterEntity record)
        {
            var collection = _database.GetCollection<ChapterEntity>(nameof(ChapterEntity));
            collection.InsertOne(record);
        }

        public ChapterEntity Get(string bookCode, int chapterId, int? verse)
        { 
            var collection = _database.GetCollection<ChapterEntity>(nameof(ChapterEntity));
            ChapterEntity chapter = collection.AsQueryable().Where(x => x.BookCode == bookCode && x.ChapterNumber == chapterId).FirstOrDefault();

            if(verse.HasValue)
            {
                int startVerse = chapter.Verses.Min(x => x.VerseNumber);
                int thisVerse = startVerse + (verse.Value - 1);

                chapter.Verses.RemoveAll(x => x.VerseNumber != thisVerse);
            }
            
            return chapter;
        }

        public ChapterEntity Next(string bookCode, int verse)
        { 
            return GetNearestVerse(bookCode, verse + 1);
        }

        public ChapterEntity First(string bookCode)
        { 
            return Get(bookCode, 1, 1);
        }

        public ChapterEntity Last(string bookCode)
        { 
            int chapterId = LastChapterId(bookCode);
            int verseId = LastVerseId(bookCode);
            return Get(bookCode, chapterId, verseId);
        }

        public (ChapterEntity, VerseEntity) Quote(string bookCode)
        { 
            var lastVerse = LastVerseId(bookCode);

            if(lastVerse == 0) return (BlankChapter(), BlankChapter().Verses.First());

            var rnd = new Random();
           
            int randomVerse = rnd.Next(1, lastVerse);
            var randomChapter = GetNearestVerse(bookCode, randomVerse);

            return (randomChapter, randomChapter.Verses.AsQueryable().First());
        }

        private ChapterEntity GetNearestVerse(string bookCode, int verse)
        {
            var collection = _database.GetCollection<ChapterEntity>(nameof(ChapterEntity));
            var chapter = collection.AsQueryable().Where(x => x.BookCode == bookCode
                        && x.Verses.Any(y => y.VerseNumber >= verse))
                        .OrderBy(x => x.ChapterNumber)
                        .FirstOrDefault();

            if(chapter != null){
                 var selectedVerse = chapter.Verses
                                        .Where(x => x.VerseNumber >= verse)
                                        .OrderBy(x => x.VerseNumber)
                                        .FirstOrDefault();

                var selectedVerseId = selectedVerse?.VerseNumber ?? 0;
                chapter.Verses.RemoveAll(x => x.VerseNumber != selectedVerseId);
            }

            if(chapter == null || !chapter.Verses.Any()){
                return Last(bookCode);
            }

            return chapter;
        }

        private int LastChapterId(string bookCode){
            
            var collection = _database.GetCollection<ChapterEntity>(nameof(ChapterEntity));
            
            var maxChapter = collection.AsQueryable()
                .Where(x => x.BookCode == bookCode)
                .OrderByDescending(x => x.ChapterNumber)
                .Select(x => x.ChapterNumber)
                .FirstOrDefault();

            return maxChapter;
        }

        private int LastVerseId(string bookCode){
            
            var collection = _database.GetCollection<ChapterEntity>(nameof(ChapterEntity));
            
            var verses = collection.AsQueryable()
                .Where(x => x.BookCode == bookCode)
                .OrderByDescending(x => x.ChapterNumber)
                .SelectMany(x => x.Verses).ToList();


            var maxVerse = verses.OrderByDescending(x => x.VerseNumber).FirstOrDefault();

            return maxVerse?.VerseNumber ?? 0;
        }

        private ChapterEntity BlankChapter(){

            return new ChapterEntity{ Verses = new List<VerseEntity>{ new VerseEntity{ Text = "No verse found" }}};
        }

        public List<(ChapterEntity chapter, VerseEntity verse)> Quotes(int numberOfQuotes)
        {
            throw new NotImplementedException();
        }
    }
}