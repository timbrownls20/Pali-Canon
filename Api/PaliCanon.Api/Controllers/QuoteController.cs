using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaliCanon.Common;
using PaliCanon.Common.Model;
using PaliCanon.Common.Repository;

namespace PaliCanon.Api.Controllers
{
    [Route("api/[controller]")]
    public class QuoteController : Controller
    {

        IChapterRepository _chapterRepository;
        IBookRepository _bookRepository;


        public QuoteController()
        {
            var database = new DBConnect().Connect();
            _chapterRepository = new ChapterRepository(database);
            _bookRepository = new BookRepository();
        }

        [HttpGet]
        public Chapter Get()
        {
            var bookCode =_bookRepository.Random();
            return _chapterRepository.Quote(bookCode);        
        }

        [HttpGet("{bookCode}")]
        public Chapter Get(string bookCode)
        {
            return _chapterRepository.Quote(bookCode);        
        }

        [HttpGet("next/{bookCode}/{chapter}/{verse}")]
        public Chapter Next(string bookCode, int chapter, int verse)
        {
            return _chapterRepository.Next(bookCode, chapter, verse);        
        }
   
        [HttpGet("first/{bookCode}")]
        public Chapter First(string bookCode)
        {
            return _chapterRepository.First(bookCode);        
        }

        [HttpGet("last/{bookCode}")]
        public Chapter Last(string bookCode)
        {
            return _chapterRepository.Last(bookCode);        
        }

    }
}
