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

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("version")]
        public string Version()
        {
            return $"Sutta API version {_config.GetValue<string>("Api:Version")}";
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
        public Chapter Get(string bookCode, int chapter, int? verse)
        {
            return _chapterService.Get(bookCode, chapter, verse);
        }

        /// <summary>
        /// Gets the next verse for a given verse. 
        /// </summary>
        /// <param name="bookCode">dhp - dhammpada</param>
        /// <param name="verse">verse number</param>
        /// <returns></returns>
        [HttpGet("next/{bookCode}/{verse}")]
        public Chapter Next(string bookCode, int verse)
        {
            return _chapterService.Next(bookCode, verse);
        }

        /// <summary>
        /// Returns the first verse for a given book
        /// </summary>
        /// <param name="bookCode">dhp - dhammpada</param>
        /// <returns></returns>
        [HttpGet("first/{bookCode}")]
        public Chapter First(string bookCode)
        {
            return _chapterService.First(bookCode);
        }

        /// <summary>
        /// Returns the last verse for a given book
        /// </summary>
        /// <param name="bookCode">dhp - dhammpada</param>
        /// <returns></returns>
        [HttpGet("last/{bookCode}")]
        public Chapter Last(string bookCode)
        {
            return _chapterService.Last(bookCode);
        }
    }
}
