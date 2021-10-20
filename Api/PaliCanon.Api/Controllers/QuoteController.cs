using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaliCanon.Contracts.Book;
using PaliCanon.Contracts.Chapter;

namespace PaliCanon.Api.Controllers
{
    /// <summary>
    /// Quote api
    /// </summary>
    [Route("api/quote")]
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
        [HttpGet("version")]
        public IActionResult Version()
        {
            return Ok($"Quote API version {_config.GetValue<string>("Api:Version")}");
        }

        /// <summary>
        /// Gets a random quote 
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "Quote")]
        public IActionResult Get()
        {
            var book = _bookService.Random();

            if (book == null) return StatusCode(StatusCodes.Status500InternalServerError, "data unavailable");

            return Ok(_chapterService.Quote(book.Code));
        }

        /// <summary>
        /// Gets a random quote from a specific book
        /// </summary>
        /// <param name="bookCode">dhp - dhammpada</param>
        /// <returns></returns>
        [HttpGet("{bookCode}", Name = "QuoteByBook")]
        public IActionResult Get(string bookCode)
        {
            return Ok(_chapterService.Quote(bookCode));
        }

        /// <summary>
        /// Gets a random set of quotes
        /// </summary>
        /// <param name="numberOfQuotes">number of quotes returned</param>
        /// <returns></returns>
        [HttpGet("{numberOfQuotes:int}", Name = "Quotes")]
        public IActionResult GetQuotes(int numberOfQuotes)
        {
            return Ok(_chapterService.Quotes(numberOfQuotes));
        }
    }
}
