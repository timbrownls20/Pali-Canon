using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaliCanon.Contracts.Book;
using PaliCanon.Contracts.Chapter;

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
        public IActionResult Version()
        {
            return Ok($"Quote API version {_config.GetValue<string>("Api:Version")}");
        }

        /// <summary>
        /// Returns random quote
        /// </summary>
        /// <param name="maxLength">Maximum character length of the quote</param>
        /// <returns></returns>
        [HttpGet("quote/{maxLength:int?}", Name = "Quote")]
        public IActionResult Get(int? maxLength = null)
        {
            return Ok(_chapterService.Quote(maxLength:maxLength));
        }

        /// <summary>
        /// Returns random quote for a book
        /// </summary>
        /// <param name="bookCode">Book code e.g. dhp</param>
        /// <param name="maxLength">Maximum character length of the quote</param>
        /// <returns></returns>
        [HttpGet("quote/{bookCode:alpha}/{maxLength:int?}", Name = "QuoteByBook")]
        public IActionResult Get(string bookCode, int? maxLength = null)
        {
            return Ok(_chapterService.Quote(bookCode, maxLength));
        }

        /// <summary>
        /// Gets a random set of quotes
        /// </summary>
        /// <param name="numberOfQuotes">number of quotes returned</param>
        /// <param name="maxLength">Maximum character length of the quote</param>
        /// <returns></returns>
        [HttpGet("quotes/{numberOfQuotes:int}/{maxLength:int?}", Name = "Quotes")]
        public IActionResult GetQuotes(int numberOfQuotes, int? maxLength)
        {
            return Ok(_chapterService.Quotes(numberOfQuotes, maxLength));
        }
    }
}
