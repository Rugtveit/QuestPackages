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
        private List<string> _packagesIdsToDelete { get; set; }
        private List<string> _dbPackageIds { get; set; }
        public CachingService(PackageAPIService packageAPIService, PackageDBService packageDBService) 
        {
            _packageAPIService = packageAPIService;
            _packageDBService = packageDBService;
        }

        public async Task UpdateCache() 
        {
            await ComparePackages();
            await UpdatePackages(_dbPackageIds);
            DeletePackages();
        }

        public async Task UpdatePackages(List<string> packageIds) 
        {
            foreach(string packageId in packageIds) 
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

        public void DeletePackages() 
        {
            if (_packagesIdsToDelete.Count == 0) return;
            foreach(var packageId in _packagesIdsToDelete) 
            {
                _packageDBService.DeletePackage(packageId);
            }
        }

        public async Task ComparePackages() 
        {
            _dbPackageIds = await _packageAPIService.GetPackageIds();
            List<string> dbPackageIds = _packageDBService.GetPackageIds();

            _packagesIdsToDelete = _dbPackageIds.Except(dbPackageIds).ToList();
        }
    }
}
