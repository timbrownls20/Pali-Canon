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

        IChapterRepository chapterRepository;

        public QuoteController()
        {
            //..TB TODO implement windsor
            var database = new DBConnect().Connect();
            chapterRepository = new ChapterRepository(database);
        }

        [HttpGet("{bookCode}")]
        public Chapter Get(string bookCode)
        {
            return chapterRepository.Quote(bookCode);        
        }

    
    }
}
