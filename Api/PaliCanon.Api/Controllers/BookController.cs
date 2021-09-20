using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PaliCanon.Common.Contracts;
using PaliCanon.Model;

namespace PaliCanon.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        
        [HttpGet]
        public string Index()
        {
            return "Book API available";
        }

        [HttpGet]
        public List<Book> List()
        {
            return _bookRepository.List();
        }

        [HttpGet]
        public Book Random()
        {
            return _bookRepository.Random();
        }
        
    }
}
