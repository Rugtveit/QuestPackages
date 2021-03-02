using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestPackages.Models;
using QuestPackages.Services;

namespace QuestPackages.Services
{
    public class CachingService
    {
        private readonly PackageAPIService _packageAPIService;
        private readonly PackageDBService _packageDBService; 
        public CachingService(PackageAPIService packageAPIService, PackageDBService packageDBService) 
        {
            _packageAPIService = packageAPIService;
            _packageDBService = packageDBService;
        }

        public async Task UpdateCache() 
        {
            List<string> packageIds = await _packageAPIService.GetPackageIds();
            foreach(var packageId in packageIds) 
            {
                DBPackage dbPackage = new DBPackage();
                List<string> packageVersions = await _packageAPIService.GetPackageVersions(packageId);

                if (_packageDBService.GetPackageVersion(packageId) == packageVersions[0]) return;
                
                Package package = await _packageAPIService.GetPackage(packageId, packageVersions[0]);
               

                dbPackage = _packageDBService.PackageToDBPackage(package);
                dbPackage.Versions = packageVersions.ToArray();
                if (_packageDBService.HasPackage(packageId)) _packageDBService.UpdatePackage(packageId, dbPackage);
                else _packageDBService.CreatePackage(dbPackage);
            }
        }
    }
}
