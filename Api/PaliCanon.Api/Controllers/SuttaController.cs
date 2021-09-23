using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PaliCanon.Contracts;
using PaliCanon.Model;

namespace PaliCanon.Api.Controllers
{
    [Route("api/[controller]")]
    public class SuttaController : ControllerBase
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
