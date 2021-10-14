using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaliCanon.Contracts.Book;
using PaliCanon.Model;

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
        public string Version()
        {
            return $"Book API version {_config.GetValue<string>("Api:Version")}";
        }

        /// <summary>
        /// Find book from book code
        /// </summary>
        /// <param name="bookCode">dhp - dhammpada</param>
        /// <returns></returns>
        [HttpGet("find/{bookCode}")]
        public Book Get(string bookCode)
        {
            return _bookService.Get(bookCode);
        }

        /// <summary>
        /// List all books
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public List<Book> List()
        {
            return _bookService.List();
        }

        /// <summary>
        /// Select a random book
        /// </summary>
        /// <returns></returns>
        [HttpGet("random")]
        public Book Random()
        {
            return _bookService.Random();
        }

    }
}
