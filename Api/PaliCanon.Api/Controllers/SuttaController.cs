using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PaliCanon.Common;
using PaliCanon.Common.Model;
using PaliCanon.Common.Repository;

namespace PaliCanon.Api.Controllers
{
    [Route("api/[controller]")]
    public class SuttaController : Controller
    {
        IChapterRepository chapterRepository;

        public SuttaController()
        {
            //..TB TODO implement windsor
            var database = new DBConnect().Connect();
            chapterRepository = new ChapterRepository(database);
        }

        [HttpGet("{bookCode}/{chapter?}/{verse?}")]
        public List<Chapter> Get(string bookCode, int? chapter, int? verse)
        {
            return chapterRepository.Get(bookCode, chapter, verse);        
        }

    
    }
}
