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
        public CacheController(CachingService cachingService)
        {
            _cachingService = cachingService;
        }


        [HttpGet("update")]
        public async Task<ActionResult> UpdateCache()
        {
            await _cachingService.UpdateCache();
            return NoContent();
        }

        [HttpGet("start")]
        public async Task<ActionResult> StartCache()
        {
            await _cachingService.StartCaching();
            return Content("Started caching!");
        }

    }
}
