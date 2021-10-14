using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PaliCanon.Common;
using PaliCanon.Contracts.Chapter;
using PaliCanon.Contracts.Quote;
using PaliCanon.Data.Sql.Entities;

namespace PaliCanon.Data.Sql.Repositories
{
    public class ChapterRepository: IChapterRepository<ChapterEntity>, IQuoteRepository<ChapterEntity, VerseEntity>
    {
        private readonly IMapper _mapper;
        private readonly SqlContext _context;

        public ChapterRepository(IMapper mapper, SqlContext context)
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
                entity.Book = book;
                entity.Author = author ?? new AuthorEntity
                {
                    Name = chapter.Author.Name
                };

                _context.Chapters.Add(entity);
                _context.SaveChanges(); 
            }
        }

        public ChapterEntity Get(string bookCode, int chapterId, int? verse)
        {
            ChapterEntity chapter = _context.Chapters.Where(x => x.Book.Code == bookCode && x.ChapterNumber == chapterId).SingleOrDefault();
            
            if (verse.HasValue)
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

            ChapterEntity chapter = _context.Chapters.Where(x => x.Book.Code == bookCode && x.ChapterNumber == chapterId).SingleOrDefault();
            chapter.Verses.RemoveAll(x => x.VerseNumber != verseId);
            
            return chapter;
        }

        public (ChapterEntity, VerseEntity) Quote(string bookCode)
        {
            int lastVerse = LastVerseId(bookCode);

            if(lastVerse == 0) return BlankChapter();

            var rnd = new Random();
           
            int randomVerse = rnd.Next(1, lastVerse);
            ChapterEntity randomChapter = GetNearestVerse(bookCode, randomVerse);

            return (randomChapter, randomChapter.Verses.First());
        }

        public List<(ChapterEntity, VerseEntity)> Quotes(int numberOfQuotes)
        {
            var quotes = new List<(ChapterEntity, VerseEntity)>();
            List<int> verses = _context.Verses.OrderBy(x => x.Id).Select(x => x.Id).ToList();
            var shuffler = new Shuffler();
            shuffler.Shuffle(verses);
            var randomVerses = verses.Take(numberOfQuotes);

            foreach(var verseId in randomVerses)
            {
                var verse = _context.Verses.First(x => x.Id == verseId);
                quotes.Add((verse.Chapter, verse));
            }

            return quotes;
        }

        public List<(ChapterEntity, VerseEntity)> Search(string searchTerm, int? pageSize, int? pageNumber = 1)
        {
            var quotes = new List<(ChapterEntity, VerseEntity)>();

            if (string.IsNullOrWhiteSpace(searchTerm)) return quotes;

            IQueryable<VerseEntity> query = _context.Verses.Where(x => x.Text.Contains(searchTerm));

            if(pageSize.HasValue)
            {
                int skip = pageNumber > 0 ? pageNumber.Value - 1 : 0;
                query = query.Skip(skip).Take(pageSize.Value);
            }

            var verses = query.ToList();
            foreach (var verse in verses)
                quotes.Add((verse.Chapter, verse));
            
            return quotes;
        }

        private ChapterEntity GetNearestVerse(string bookCode, int verse)
        {
            ChapterEntity chapter = _context.Chapters
                        .Where(x => x.Book.Code == bookCode
                        && x.Verses.Any(y => y.VerseNumber >= verse))
                        .OrderBy(x => x.ChapterNumber)
                        .FirstOrDefault();

            if (chapter != null)
            {
                VerseEntity selectedVerse = chapter.Verses
                                       .Where(x => x.VerseNumber >= verse)
                                       .OrderBy(x => x.VerseNumber)
                                       .FirstOrDefault();

                int selectedVerseId = selectedVerse?.VerseNumber ?? 0;
                chapter.Verses.RemoveAll(x => x.VerseNumber != selectedVerseId);
            }

            if (chapter == null || !chapter.Verses.Any())
            {
                return Last(bookCode);
            }

            return chapter;
        }

        private int LastChapterId(string bookCode){

            int maxChapter = _context.Chapters
                .Where(x => x.Book.Code == bookCode)
                .OrderByDescending(x => x.ChapterNumber)
                .Select(x => x.ChapterNumber)
                .FirstOrDefault();

            return maxChapter;
        }

        private int LastVerseId(string bookCode){

            List<VerseEntity> verses = _context.Chapters
                .Where(x => x.Book.Code == bookCode)
                .OrderByDescending(x => x.ChapterNumber)
                .SelectMany(x => x.Verses).ToList();

            VerseEntity maxVerse = verses.OrderByDescending(x => x.VerseNumber).FirstOrDefault();

            return maxVerse?.VerseNumber ?? 0;
        }

        private (ChapterEntity, VerseEntity) BlankChapter(){

            return ( new ChapterEntity(), new VerseEntity { Text = "No verse found" });
        }

        public List<(ChapterEntity chapter, VerseEntity verse)> Search(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
