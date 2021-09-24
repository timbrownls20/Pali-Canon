using System.Collections.Generic;
using AutoMapper;
using PaliCanon.Contracts;
using PaliCanon.Model;

namespace PaliCanon.Services
{
    public class ChapterService<T>: IChapterService where T : class
    {
        private readonly IChapterRepository<T> _chapterRepository;
        private readonly IMapper _mapper;

        public ChapterService(IChapterRepository<T> chapterRepository, IMapper mapper)
        {
            _chapterRepository = chapterRepository;
            _mapper = mapper;
        }
        
        public void Insert(Chapter record)
        {
            _chapterRepository.Insert(_mapper.Map<T>(record));
        }

        public List<Chapter> Get(string bookCode, int? chapter, int? verse)
        {
            return _mapper.Map<List<Chapter>>(_chapterRepository.Get(bookCode, chapter, verse));
        }

        public Quote Quote(string bookCode)
        {
            return _mapper.Map<Quote>(_chapterRepository.Quote(bookCode));
        }

        public Chapter Next(string bookCode, int chapter, int verse)
        {
            return _mapper.Map<Chapter>(_chapterRepository.Next(bookCode, chapter, verse));
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
