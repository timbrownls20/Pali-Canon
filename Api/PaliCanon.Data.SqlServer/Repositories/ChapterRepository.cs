using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaliCanon.Contracts;
using PaliCanon.Data.SqlServer.Entities;
using PaliCanon.Model;

namespace PaliCanon.Data.SqlServer.Repositories
{
    public class ChapterRepository: IChapterRepository
    {
        private readonly IMapper _mapper;
        private readonly SqlServerContext _context;

        public ChapterRepository(IMapper mapper, SqlServerContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Insert(Chapter chapter)
        {
            var book = _context.Books.First(x => x.Code == chapter.BookCode);
            
            var chapterEntity = _context.Chapters.FirstOrDefault(x => x.ChapterNumber == chapter.ChapterNumber && x.BookId == book.Id);
            if (chapterEntity == null)
            {
                var entity = _mapper.Map<ChapterEntity>(chapter);
                entity.BookId = book.Id;
                _context.Chapters.Add(entity);
            }
            else
            {
                _mapper.Map(chapter, chapterEntity);
                //entity.Id = chapterEntity.Id;
                chapterEntity.BookId = book.Id;

                //_context.Chapters.
                //_context.Entry(entity).State = EntityState.Modified;
                _context.Chapters.Attach(chapterEntity);
            }

            _context.SaveChanges(); //.. TB TODO implement unit of work
        }

        public List<Chapter> Get(string bookCode, int? chapterId, int? verse)
        { 
            throw new NotImplementedException();

            //var collection = _database.GetCollection<ChapterEntity>(nameof(ChapterEntity));
            //var query = collection.AsQueryable().Where(x => x.BookCode == bookCode);
            //List<ChapterEntity> chapters = new List<ChapterEntity>();

            //if(chapterId.HasValue)
            //{
            //    var chapter = query.SingleOrDefault(x => x.ChapterNumber == chapterId);
            //    if(chapter != null)
            //    {
            //        if(verse.HasValue)
            //        {
            //            chapter.Verses.RemoveAll(x => x.VerseNumber != verse);
            //        }
            //        chapters.Add(chapter);
            //    }
            //}
            //else
            //{
            //    chapters = query.ToList();
            //}
            
            //return _mapper.Map<List<Chapter>>(chapters);
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

            var rnd = new Random();
           
            int randomVerse = rnd.Next(1, lastVerse);
            var randomChapter = GetNearestVerse(bookCode, randomVerse);

            return randomChapter;
        }

        private Chapter GetNearestVerse(string bookCode, int verse)
        {
            throw new NotImplementedException();

            //var collection = _database.GetCollection<ChapterEntity>(nameof(ChapterEntity));
            //var chapter = collection.AsQueryable().Where(x => x.BookCode == bookCode
            //            && x.Verses.Any(y => y.VerseNumber >= verse))
            //            .OrderBy(x => x.ChapterNumber)
            //            .FirstOrDefault();

            //if(chapter != null){
            //     var selectedVerse = chapter.Verses
            //                            .Where(x => x.VerseNumber >= verse)
            //                            .OrderBy(x => x.VerseNumber)
            //                            .FirstOrDefault();

            //    var selectedVerseId = selectedVerse?.VerseNumber ?? 0;
            //    chapter.Verses.RemoveAll(x => x.VerseNumber != selectedVerseId);
            //}

            //if(chapter == null || !chapter.Verses.Any()){
            //    return Last(bookCode);
            //}

            //return _mapper.Map<Chapter>(chapter);
        }

        private int LastChapterId(string bookCode){

            throw new NotImplementedException();

            //var collection = _database.GetCollection<ChapterEntity>(nameof(ChapterEntity));

            //var maxChapter = collection.AsQueryable()
            //    .Where(x => x.BookCode == bookCode)
            //    .OrderByDescending(x => x.ChapterNumber)
            //    .Select(x => x.ChapterNumber)
            //    .FirstOrDefault();

            //return maxChapter;
        }

        private int LastVerseId(string bookCode){

            throw new NotImplementedException();

            //var collection = _database.GetCollection<ChapterEntity>(nameof(ChapterEntity));

            //var verses = collection.AsQueryable()
            //    .Where(x => x.BookCode == bookCode)
            //    .OrderByDescending(x => x.ChapterNumber)
            //    .SelectMany(x => x.Verses).ToList();


            //var maxVerse = verses.OrderByDescending(x => x.VerseNumber).FirstOrDefault();

            //return maxVerse?.VerseNumber ?? 0;
        }

        private Chapter BlankChapter(){

            return _mapper.Map<Chapter>(new ChapterEntity{ Verses = new List<VerseEntity>{ new VerseEntity{ Text = "No verse found" }}});
        }
    }
}