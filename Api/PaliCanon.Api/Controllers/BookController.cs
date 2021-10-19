using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaliCanon.Contracts.Book;

namespace PaliCanon.Api.Controllers
{
    /// <summary>
    /// Pali canon book api
    /// </summary>
    [ApiController]
    [Route("api/book")]
    public class BookController: ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IBookService _bookService;

        public BookController(IConfiguration config, IBookService bookService)
        {
            _config = config;
            _bookService = bookService;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [HttpGet("version")]
        public IActionResult Version()
        {
            return Ok($"Book API version {_config.GetValue<string>("Api:Version")}");
        }

        /// <summary>
        /// Find book from book code
        /// </summary>
        /// <param name="bookCode">dhp - dhammpada</param>
        /// <returns></returns>
        [HttpGet("find/{bookCode}", Name = "GetBook")]
        public IActionResult Get(string bookCode)
        {
            return Ok(_bookService.Get(bookCode));
        }

        /// <summary>
        /// List all books
        /// </summary>
        /// <returns></returns>
        [HttpGet("list", Name = "ListBooks")]
        public IActionResult List()
        {
            return Ok(_bookService.List());
        }

        /// <summary>
        /// Select a random book
        /// </summary>
        /// <returns></returns>
        [HttpGet("random", Name = "RandomBook")]
        public IActionResult Random()
        {
            return Ok(_bookService.Random());
        }

    }
}
