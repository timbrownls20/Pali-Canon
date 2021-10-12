using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaliCanon.Contracts.Book;
using PaliCanon.Contracts.Chapter;
using PaliCanon.Model;

namespace PaliCanon.Api.Controllers
{
    [Route("api/[controller]")]
    public class QuoteController : ControllerBase
    {
        readonly IChapterService _chapterService;
        private readonly IConfiguration _config;
        readonly IBookService _bookService;

        public QuoteController(IConfiguration config, IBookService bookService, IChapterService chapterService)
        {
            _chapterService = chapterService;
            _config = config;
            _bookService = bookService;
        }

        [HttpGet("version")]
        public string Version()
        {
            return $"Quote API version {_config.GetValue<string>("Api:Version")}";
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
