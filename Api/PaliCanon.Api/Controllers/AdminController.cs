using Microsoft.AspNetCore.Mvc;
using PaliCanon.Common.Enums;
using PaliCanon.Contracts;
using PaliCanon.DataLoad.Provider.Factory;

namespace PaliCanon.Api.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IProviderFactory _providerFactory;
        private readonly IAdminRepository _adminRepository;

        public AdminController(IProviderFactory _providerFactory, IAdminRepository adminRepository)
        {
            this._providerFactory = _providerFactory;
            _adminRepository = adminRepository;
        }

        [HttpGet]
        public string Available()
        {
            return "Admin API available version 0.1";
        }

        [HttpGet("{book}")]
        public string Load(Book book)
        {
            var provider = _providerFactory.Get(book);
            provider.Load();
            return $"{book} has been loaded";
        }

        [HttpGet]
        public bool CanConnect()
        {
            bool canConnect = _adminRepository.CanConnect();
            return canConnect;
        }
    }
}
