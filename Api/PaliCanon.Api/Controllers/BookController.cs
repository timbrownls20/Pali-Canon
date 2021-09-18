using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PaliCanon.Common.Repository;
using PaliCanon.Common.Model;

namespace PaliCanon.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BookController : Controller
    {
        private IBookRepository _bookRepository;

        public BookController()
        {
            _bookRepository = new BookRepository();
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
