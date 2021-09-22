using Microsoft.AspNetCore.Mvc;
using PaliCanon.Common.Enums;
using PaliCanon.DataLoad.Provider;

namespace PaliCanon.Api.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IProviderFactory _providerFactory;

        public AdminController(IProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
        }

        [HttpGet("index")]
        public string Index()
        {
            return "Admin API available";
        }

        [HttpGet("{book}")]
        public string Load(Book book)
        {
            var provider = _providerFactory.Get(book);
            provider.Load();
            return $"{book} has been loaded";
        }
    }
}
