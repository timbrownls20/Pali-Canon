using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaliCanon.Contracts.Chapter;

namespace PaliCanon.Api.Controllers
{
    /// <summary>
    /// Quote api
    /// </summary>
    [Route("api")]
    public class SearchController : ControllerBase
    {
        readonly IChapterService _chapterService;
        private readonly IConfiguration _config;
        
        public SearchController(IConfiguration config, IChapterService chapterService)
        {
            _chapterService = chapterService;
            _config = config;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("search/version")]
        public IActionResult Version()
        {
            return Ok($"Search API version {_config.GetValue<string>("Api:Version")}");
        }

        /// <summary>
        /// Gets all quotes that contain the search term
        /// </summary>
        /// <param name="searchTerm">search term</param>
        /// <param name="pageSize">number of results to return. If omitted then all results returned</param>
        /// <param name="pageNumber">page number for results. If omitted then defaults to 1</param>
        /// <returns></returns>
        [HttpGet("search/{searchTerm}/{pageSize:int?}/{pageNumber:int?}", Name = "Search")]
        public IActionResult Search(string searchTerm, int? pageSize, int? pageNumber)
        {
            return Ok(_chapterService.Search(searchTerm, pageSize, pageNumber));
        }
    }
}
