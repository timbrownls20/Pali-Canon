﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaliCanon.Common.Enums;
using PaliCanon.Contracts;
using PaliCanon.DataLoad.Provider.Factory;

namespace PaliCanon.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IProviderFactory _providerFactory;
        private readonly IAdminRepository _adminRepository;

        public AdminController(IConfiguration config, IProviderFactory providerFactory, IAdminRepository adminRepository)
        {
            _config = config;
            _providerFactory = providerFactory;
            _adminRepository = adminRepository;
        }

        [HttpGet]
        [HttpGet("version")]
        public IActionResult Version()
        {
            return Ok($"Admin API version {_config.GetValue<string>("Api:Version")}");
        }

#if DEBUG
        [HttpGet("load/{book}")]
        public IActionResult Load(Book book)
        {
            var provider = _providerFactory.Get(book);
            provider.Load();
            return Ok($"{book} has been loaded");
        }
#endif

        [HttpGet("canconnect")]
        public IActionResult CanConnect()
        {
            bool canConnect = _adminRepository.CanConnect();
            return Ok(canConnect);
        }
    }
}
