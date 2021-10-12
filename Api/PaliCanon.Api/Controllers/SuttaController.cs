using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaliCanon.Contracts.Chapter;
using PaliCanon.Model;

namespace PaliCanon.Api.Controllers
{
    [Route("api/[controller]")]
    public class SuttaController : ControllerBase
    {
        private readonly IConfiguration _config;
        readonly IChapterService _chapterService;

        public SuttaController(IConfiguration config, IChapterService chapterService)
        {
            _config = config;
            _chapterService = chapterService;
        }

        [HttpGet]
        [HttpGet("version")]
        public string Version()
        {
            return $"Sutta API version {_config.GetValue<string>("Api:Version")}";
        }

        [HttpGet("{bookCode}/{chapter?}/{verse?}")]
        public List<Chapter> Get(string bookCode, int? chapter, int? verse)
        {
            return _chapterService.Get(bookCode, chapter, verse);        
        }

        [HttpGet("next/{bookCode}/{chapter}/{verse}")]
        public Chapter Next(string bookCode, int chapter, int verse)
        {
            return _chapterService.Next(bookCode, chapter, verse);
        }

        [HttpGet("first/{bookCode}")]
        public Chapter First(string bookCode)
        {
            return _chapterService.First(bookCode);
        }

        [HttpGet("last/{bookCode}")]
        public Chapter Last(string bookCode)
        {
            return _chapterService.Last(bookCode);
        }
    }
}
