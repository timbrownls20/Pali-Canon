using Microsoft.AspNetCore.Mvc;
using PaliCanon.Contracts;
using PaliCanon.Model;

namespace PaliCanon.Api.Controllers
{
    [Route("api/[controller]")]
    public class QuoteController : Controller
    {
        readonly IChapterRepository _chapterRepository;
        readonly IBookRepository _bookRepository;

        public QuoteController(IBookRepository bookRepository, IChapterRepository chapterRepository)
        {
            _chapterRepository = chapterRepository;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public Chapter Get()
        {
            var book =_bookRepository.Random();
            return _chapterRepository.Quote(book.Code);        
        }

        [HttpGet("{bookCode}")]
        public Chapter Get(string bookCode)
        {
            return _chapterRepository.Quote(bookCode);        
        }

        [HttpGet("next/{bookCode}/{chapter}/{verse}")]
        public Chapter Next(string bookCode, int chapter, int verse)
        {
            return _chapterRepository.Next(bookCode, chapter, verse);        
        }
   
        [HttpGet("first/{bookCode}")]
        public Chapter First(string bookCode)
        {
            return _chapterRepository.First(bookCode);        
        }

        [HttpGet("last/{bookCode}")]
        public Chapter Last(string bookCode)
        {
            return _chapterRepository.Last(bookCode);        
        }
    }
}
