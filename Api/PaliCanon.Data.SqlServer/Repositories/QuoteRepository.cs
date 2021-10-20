using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PaliCanon.Common;
using PaliCanon.Contracts.Quote;
using PaliCanon.Data.Sql.Entities;

namespace PaliCanon.Data.Sql.Repositories
{
    public class QuoteRepository: IQuoteRepository<ChapterEntity, VerseEntity>
    {
        private readonly SqlContext _context;

        public QuoteRepository(SqlContext context)
        {
            _context = context;
        }

        public (ChapterEntity, VerseEntity) Quote(string bookCode)
        {
            return Quotes(1, bookCode).FirstOrDefault();
        }

        public List<(ChapterEntity, VerseEntity)> Quotes(int numberOfQuotes, string bookCode = null)
        {
            var quotes = new List<(ChapterEntity, VerseEntity)>();
            var versesQuery = string.IsNullOrWhiteSpace(bookCode) ? 
                                    _context.Verses 
                                    : _context.Verses.Where(x => x.Chapter.Book.Code == bookCode);
                
            var verses = versesQuery.OrderBy(x => x.Id).Select(x => x.Id).ToList();
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

            var verses = query.OrderBy(x => x.Chapter.Book.Description)
                                .ThenBy(x => x.ChapterId)
                                .ThenBy(x => x.VerseNumber)
                                .ToList();

            foreach (var verse in verses)
                quotes.Add((verse.Chapter, verse));
            
            return quotes;
        }
    }
}
