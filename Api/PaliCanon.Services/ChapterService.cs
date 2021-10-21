using System.Collections.Generic;
using AutoMapper;
using PaliCanon.Contracts.Chapter;
using PaliCanon.Contracts.Quote;
using PaliCanon.Contracts.Verse;
using PaliCanon.Model;

namespace PaliCanon.Services
{
    public class ChapterService<T, U>: IChapterService where T : class, IChapterEntity
                                        where U: class, IVerseEntity
    {
        private readonly IChapterRepository<T> _chapterRepository;
        private readonly IQuoteRepository<T, U> _quoteRepository;
        private readonly IMapper _mapper;

        public ChapterService(IChapterRepository<T> chapterRepository,
                                IQuoteRepository<T, U> quoteRepository,
                                IMapper mapper)
        {
            _chapterRepository = chapterRepository;
            _quoteRepository = quoteRepository;
            _mapper = mapper;
        }
        
        public void Insert(Chapter record)
        {
            _chapterRepository.Insert(_mapper.Map<T>(record));
        }

        public Chapter Get(string bookCode, int chapter, int? verse)
        {
            return _mapper.Map<Chapter>(_chapterRepository.Get(bookCode, chapter, verse));
        }

        public Quote Quote(string bookCode = null, int? maxLength = null)
        {
            return _mapper.Map<Quote>(_quoteRepository.Quote(bookCode, maxLength));
        }

        public List<Quote> Quotes(int numberOfQuotes, int? maxLength = null)
        {
            return _mapper.Map<List<Quote>>(_quoteRepository.Quotes(numberOfQuotes, maxLength:maxLength));
        }

        public List<Quote> Search(string searchTerm, int? pageSize, int? pageNumber = 1)
        {
            return _mapper.Map<List<Quote>>(_quoteRepository.Search(searchTerm, pageSize, pageNumber));
        }

        public Chapter Next(string bookCode, int verse)
        {
            return _mapper.Map<Chapter>(_chapterRepository.Next(bookCode, verse));
        }

        public Chapter First(string bookCode)
        {
            return _mapper.Map<Chapter>(_chapterRepository.First(bookCode));
        }

        public Chapter Last(string bookCode)
        {
            return _mapper.Map<Chapter>(_chapterRepository.Last(bookCode));
        }
    }
}
