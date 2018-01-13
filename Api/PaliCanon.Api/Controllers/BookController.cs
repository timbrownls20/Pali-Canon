using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaliCanon.Common.Repository;


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
        public IEnumerable<string> Codes()
        {
            return _bookRepository.Codes();
        }

        [HttpGet]
        public string Random()
        {
            return _bookRepository.Random();
        }

      
    }
}
