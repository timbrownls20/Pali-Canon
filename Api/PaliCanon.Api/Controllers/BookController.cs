using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaliCanon.Contracts.Book;
using PaliCanon.Model;

namespace PaliCanon.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController: ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IBookService _bookService;

        public BookController(IConfiguration config, IBookService bookService)
        {
            _config = config;
            _bookService = bookService;
        }


        [HttpGet]
        [HttpGet("version")]
        public string Version()
        {
            return $"Book API version {_config.GetValue<string>("Api:Version")}";
        }

        [HttpGet("find/{bookCode}")]
        public Book Get(string bookCode)
        {
            return _bookService.Get(bookCode);
        }

        [HttpGet("list")]
        public List<Book> List()
        {
            return _bookService.List();
        }

        [HttpGet("random")]
        public Book Random()
        {
            return _bookService.Random();
        }

    }
}
