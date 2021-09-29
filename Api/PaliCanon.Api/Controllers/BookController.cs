using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PaliCanon.Contracts.Book;
using PaliCanon.Model;

namespace PaliCanon.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController: ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        
        [HttpGet("available")]
        public string Available()
        {
            return "Book API available";
        }
        
        [HttpGet]
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
