using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PaliCanon.Contracts;
using PaliCanon.Data.SqlServer.Entities;

namespace PaliCanon.Data.SqlServer.Repositories
{
    public class ChapterRepository: IChapterRepository<ChapterEntity>
    {
        private readonly IMapper _mapper;
        private readonly SqlServerContext _context;

        public ChapterRepository(IMapper mapper, SqlServerContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Insert(ChapterEntity chapter)
        {
            var book = _context.Books.FirstOrDefault(x => x.Code == chapter.Book.Code);
            if (book == null) return;

            var author = _context.Authors.FirstOrDefault(x => x.Name == chapter.Author.Name);

            var chapterEntity = _context.Chapters.FirstOrDefault(x => x.ChapterNumber == chapter.ChapterNumber && x.BookId == book.Id);
            if (chapterEntity == null)
            {
                var entity = _mapper.Map<ChapterEntity>(chapter);
                
                entity.BookId = book.Id;
                entity.Author = author ?? new AuthorEntity
                {
                    Name = chapter.Author.Name
                };

                _context.Chapters.Add(entity);
                _context.SaveChanges(); //.. TB TODO implement unit of work
            }
        }

        public List<ChapterEntity> Get(string bookCode, int? chapterId, int? verse)
        {
            var query = _context.Chapters.Where(x => x.Book.Code == bookCode);
            List<ChapterEntity> chapters = new List<ChapterEntity>();

            if (chapterId.HasValue)
            {
                var chapter = query.SingleOrDefault(x => x.ChapterNumber == chapterId);
                if (chapter != null)
                {
                    if (verse.HasValue)
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

        public ChapterEntity Next(string bookCode, int chapterId, int verse)
        { 
            return GetNearestVerse(bookCode, verse + 1);
        }

        public ChapterEntity First(string bookCode)
        { 
            return Get(bookCode, 1, 1).FirstOrDefault();
        }

        public ChapterEntity Last(string bookCode)
        { 
            int chapterId = LastChapterId(bookCode);
            int verseId = LastVerseId(bookCode);
            return Get(bookCode, chapterId, verseId).FirstOrDefault();
        }

        public ChapterEntity Quote(string bookCode)
        { 
            var lastVerse = LastVerseId(bookCode);

            if(lastVerse == 0) return BlankChapter();

            var rnd = new Random();
           
            int randomVerse = rnd.Next(1, lastVerse);
            var randomChapter = GetNearestVerse(bookCode, randomVerse);

            return randomChapter;
        }

        private ChapterEntity GetNearestVerse(string bookCode, int verse)
        {
            var chapter = _context.Chapters
                        .Where(x => x.Book.Code == bookCode
                        && x.Verses.Any(y => y.VerseNumber >= verse))
                        .OrderBy(x => x.ChapterNumber)
                        .FirstOrDefault();

            if (chapter != null)
            {
                var selectedVerse = chapter.Verses
                                       .Where(x => x.VerseNumber >= verse)
                                       .OrderBy(x => x.VerseNumber)
                                       .FirstOrDefault();

                var selectedVerseId = selectedVerse?.VerseNumber ?? 0;
                chapter.Verses.RemoveAll(x => x.VerseNumber != selectedVerseId);
            }

            if (chapter == null || !chapter.Verses.Any())
            {
                return Last(bookCode);
            }

            return chapter;
        }

        private int LastChapterId(string bookCode){

            var maxChapter = _context.Chapters
                .Where(x => x.Book.Code == bookCode)
                .OrderByDescending(x => x.ChapterNumber)
                .Select(x => x.ChapterNumber)
                .FirstOrDefault();

            return maxChapter;
        }

        private int LastVerseId(string bookCode){

            var verses = _context.Chapters
                .Where(x => x.Book.Code == bookCode)
                .OrderByDescending(x => x.ChapterNumber)
                .SelectMany(x => x.Verses).ToList();


            var maxVerse = verses.OrderByDescending(x => x.VerseNumber).FirstOrDefault();

            return maxVerse?.VerseNumber ?? 0;
        }

        private ChapterEntity BlankChapter(){

            return new ChapterEntity{ Verses = new List<VerseEntity>{ new VerseEntity{ Text = "No verse found" }}};
        }
    }
}