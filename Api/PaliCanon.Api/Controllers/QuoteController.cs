using Microsoft.AspNetCore.Mvc;
using PaliCanon.Contracts;
using PaliCanon.Contracts.Book;
using PaliCanon.Contracts.Chapter;
using PaliCanon.Model;

namespace PaliCanon.Api.Controllers
{
    [Route("api/[controller]")]
    public class QuoteController : ControllerBase
    {
        readonly IChapterService _chapterService;
        readonly IBookService _bookService;

        public QuoteController(IBookService bookService, IChapterService chapterService)
        {
            _chapterService = chapterService;
            _bookService = bookService;
        }

        [HttpGet("available")]
        public string Available()
        {
            return "Quote API available";
        }

        [HttpGet]
        public Quote Get()
        {
            var book =_bookService.Random();
            return _chapterService.Quote(book.Code);        
        }

        [HttpGet("{bookCode}")]
        public Quote Get(string bookCode)
        {
            return _chapterService.Quote(bookCode);        
        }
    }
}
