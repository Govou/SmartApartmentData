using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SAD.Core.Models;
using SAD.Infrastructure.Services.IndexingServices;
using SAD.Infrastructure.Services.SearchServices;

namespace SAD.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IManagementSearchService _mgmtSearchService;
        private readonly IManagementIndexingService _mgmtIndexingService;
        public ManagementController(IConfiguration config, IManagementSearchService mgmtSearchService, IManagementIndexingService mgmtIndexingService)
        {
            _config = config;
            _mgmtSearchService = mgmtSearchService;
            _mgmtIndexingService = mgmtIndexingService;
        }

        [HttpGet("{text}")]
        public async Task<IActionResult> Get(string text, int pageSize = 25)
        {
            var response = await _mgmtSearchService.Search(text, pageSize);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            string filePath = _config.GetValue<string>("ManagementJsonFile");
            await _mgmtIndexingService.UploadDocuments(filePath);

            return Created("", null);
        }
    }
}
