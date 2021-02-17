using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestPackages.Services;
using QuestPackages.Models;

namespace QuestPackages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly PackageDBService _packageDBService;

        public PackageController(PackageDBService packageDBService)
        {
            _packageDBService = packageDBService;
        }

        [HttpGet]
        public ActionResult<List<DBPackage>> GetPackages() => _packageDBService.GetPackages();

        [HttpGet("ids")]
        public ActionResult<List<String>> GetPackageIds() => _packageDBService.GetPackageIds();

        [HttpGet("{id}")]
        public ActionResult<DBPackage> GetPackage(string id) => _packageDBService.GetPackage(id);

    }
}
