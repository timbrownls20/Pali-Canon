using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaliCanon.Contracts.Book;
using PaliCanon.Contracts.Chapter;
using PaliCanon.Model;
using System.Collections.Generic;

namespace PaliCanon.Api.Controllers
{
    /// <summary>
    /// Quote api
    /// </summary>
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

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("quote/version")]
        public string Version()
        {
            return $"Quote API version {_config.GetValue<string>("Api:Version")}";
        }

        /// <summary>
        /// Gets a random quote 
        /// </summary>
        /// <returns></returns>
        [HttpGet("quote")]
        public Quote Get()
        {
            var book = _bookService.Random();
            return _chapterService.Quote(book.Code);
        }

        /// <summary>
        /// Gets a random quote from a specific book
        /// </summary>
        /// <param name="bookCode">dhp - dhammpada</param>
        /// <returns></returns>
        [HttpGet("quote/{bookCode}")]
        public Quote Get(string bookCode)
        {
            return _chapterService.Quote(bookCode);
        }

        /// <summary>
        /// Gets a random set of quotes
        /// </summary>
        /// <param name="numberOfQuotes">number of quotes returned</param>
        /// <returns></returns>
        [HttpGet("quotes/{numberOfQuotes}")]
        public List<Quote> GetQuotes(int numberOfQuotes)
        {
            return _chapterService.Quotes(numberOfQuotes);
        }

        /// <summary>
        /// Gets all quotes that contain the search term
        /// </summary>
        /// <param name="searchTerm">search term</param>
        /// <param name="pageSize">number of results to return. If omitted then all results returned</param>
        /// <param name="pageNumber">page number for results. If omitted then defaults to 1</param>
        /// <returns></returns>
        [HttpGet("search/{searchTerm}/{pageSize:int?}/{pageNumber:int?}")]
        public List<Quote> GetQuotes(string searchTerm, int? pageSize, int? pageNumber)
        {
            return _chapterService.Search(searchTerm, pageSize, pageNumber);
        }
    }
}
