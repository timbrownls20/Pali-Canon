using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PaliCanon.Contracts;
using PaliCanon.Model;

namespace PaliCanon.Api.Controllers
{
    [Route("api/[controller]")]
    public class SuttaController : Controller
    {
        readonly IChapterRepository _chapterRepository;

        public SuttaController(IChapterRepository chapterRepository)
        {
            _chapterRepository = chapterRepository;
        }

        [HttpGet("{bookCode}/{chapter?}/{verse?}")]
        public List<Chapter> Get(string bookCode, int? chapter, int? verse)
        {
            return _chapterRepository.Get(bookCode, chapter, verse);        
        }
    }
}
