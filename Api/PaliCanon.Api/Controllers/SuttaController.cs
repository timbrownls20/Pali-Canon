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
    public class SuttaController : Controller
    {
        // // GET api/values
        // [HttpGet]
        // public IEnumerable<string> Get()
        // {
        //     return new string[] { "value1", "value2" };
        // }

        IChapterRepository chapterRepository;

        public SuttaController()
        {
            //..TB TODO implement windsor
            var database = new DBConnect().Connect();
            chapterRepository = new ChapterRepository(database);
        }

        // GET api/values/5
        [HttpGet("{bookCode}/{chapter?}/{verse?}")]
        public List<Chapter> Get(string bookCode, int? chapter, int? verse)
        {
            return chapterRepository.Get(bookCode, chapter, verse);        
        }

    
    }
}
