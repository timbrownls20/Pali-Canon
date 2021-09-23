using Microsoft.AspNetCore.Mvc;
using PaliCanon.Contracts;
using PaliCanon.Model;

namespace PaliCanon.Api.Controllers
{
    [Route("api/[controller]")]
    public class QuoteController : ControllerBase
    {
        readonly IChapterRepository _chapterRepository;
        readonly IBookRepository _bookRepository;

        public QuoteController(IBookRepository bookRepository, IChapterRepository chapterRepository)
        {
            _chapterRepository = chapterRepository;
            _bookRepository = bookRepository;
        }

        [HttpGet("available")]
        public string Available()
        {
            return "Quote API available";
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
    }
}
