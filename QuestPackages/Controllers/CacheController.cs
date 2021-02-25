using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestPackages.Services;
using QuestPackages.Models;

namespace QuestPackages.Controllers
{
    [Route("api/cache")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly CachingService _cachingService;
        private readonly ICacheSettings _cacheSettings;
        public CacheController(CachingService cachingService, ICacheSettings cacheSettings)
        {
            _cachingService = cachingService;
            _cacheSettings = cacheSettings;
        }

        [HttpGet("{token}")]
        public async Task<ActionResult> UpdateCache(string token)
        {
            if (token != _cacheSettings.UpdateCacheToken) return NotFound();
            await _cachingService.UpdateCache();
            return Content("Updated Cache!");
        }

    }
}
