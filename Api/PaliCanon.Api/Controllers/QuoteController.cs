using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaliCanon.Contracts.Book;
using PaliCanon.Contracts.Chapter;
using PaliCanon.Model;
using System.Collections.Generic;

namespace PaliCanon.Api.Controllers
{
    [Route("api")]
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

        [HttpGet("quote/version")]
        public string Version()
        {
            return $"Quote API version {_config.GetValue<string>("Api:Version")}";
        }

        [HttpGet("quote")]
        public Quote Get()
        {
            var book =_bookService.Random();
            return _chapterService.Quote(book.Code);        
        }

        [HttpGet("quote/{bookCode}")]
        public Quote Get(string bookCode)
        {
            return _chapterService.Quote(bookCode);        
        }

        [HttpGet("quotes/{numberOfQuotes}")]
        public List<Quote> GetQuotes(int numberOfQuotes)
        {
            return _chapterService.Quotes(numberOfQuotes);
        }

        [HttpGet("search/{searchTerm}/{pageSize:int?}/{pageNumber:int?}")]
        public List<Quote> GetQuotes(string searchTerm, int? pageSize, int? pageNumber)
        {
            return _chapterService.Search(searchTerm, pageSize, pageNumber);
        }
    }
}
