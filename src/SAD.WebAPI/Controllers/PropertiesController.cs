using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SAD.Core.Models;
using SAD.Infrastructure.Services.IndexingServices;
using SAD.Infrastructure.Services.SearchServices;

namespace SAD.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IPropertySearchService _propSearchService;
        private readonly IPropertyIndexingService _propIndexingService;
        public PropertiesController(IConfiguration config, IPropertySearchService propSearchService, IPropertyIndexingService propIndexingService)
        {
            _config = config;
            _propSearchService = propSearchService;
            _propIndexingService = propIndexingService;
        }

        [HttpGet("{text}")]
        public async Task<IEnumerable<MainProperty>> Get(string text, int pageSize = 25)
        {
            return await _propSearchService.Search(text, pageSize);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            string filePath = _config.GetValue<string>("PropertyJsonFile");
            await _propIndexingService.UploadDocuments(filePath);

            return Created("", null);
        }
    }
}
