using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaliCanon.Contracts.Chapter;

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

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("version")]
        public IActionResult Version()
        {
            return Ok($"Sutta API version {_config.GetValue<string>("Api:Version")}");
        }

        /// <summary>
        /// Gets a specfic chapter from a book
        /// </summary>
        /// <param name="bookCode">dhp - dhammpada</param>
        /// <param name="chapter">chapter number</param>
        /// <param name="verse">position of verse in chapter 
        /// e.g. 2 returns the second verse in the chapter not verse with the id = 2
        /// If omitted then all verses for the chapter are returned</param>
        /// <returns></returns>
        [HttpGet("{bookCode}/{chapter}/{verse?}")]
        public IActionResult Get(string bookCode, int chapter, int? verse)
        {
            return Ok(_chapterService.Get(bookCode, chapter, verse));
        }

        /// <summary>
        /// Gets the next verse for a given verse. 
        /// </summary>
        /// <param name="bookCode">dhp - dhammpada</param>
        /// <param name="verse">verse number</param>
        /// <returns></returns>
        [HttpGet("next/{bookCode}/{verse}")]
        public IActionResult Next(string bookCode, int verse)
        {
            return Ok(_chapterService.Next(bookCode, verse));
        }

        /// <summary>
        /// Returns the first verse for a given book
        /// </summary>
        /// <param name="bookCode">dhp - dhammpada</param>
        /// <returns></returns>
        [HttpGet("first/{bookCode}")]
        public IActionResult First(string bookCode)
        {
            return Ok(_chapterService.First(bookCode));
        }

        /// <summary>
        /// Returns the last verse for a given book
        /// </summary>
        /// <param name="bookCode">dhp - dhammpada</param>
        /// <returns></returns>
        [HttpGet("last/{bookCode}")]
        public IActionResult Last(string bookCode)
        {
            return Ok(_chapterService.Last(bookCode));
        }
    }
}
