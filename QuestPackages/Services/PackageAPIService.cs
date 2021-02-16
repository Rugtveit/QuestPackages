using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using QuestPackages.Models;

namespace QuestPackages.Services
{
    public class PackageAPIService
    {
        private readonly RequestService _requestService;
        public PackageAPIService(RequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<List<string>> GetPackageIds() 
        {
            string responseString = await _requestService.GetRequest("");
            return JsonConvert.DeserializeObject<List<string>>(responseString);
        }

        public async Task<List<string>> GetPackageVersions(string packageId) 
        {
            string responseString = await _requestService.GetRequest($"{packageId}/?limit=0");
            List<SimplePackage> simplePackages = JsonConvert.DeserializeObject<List<SimplePackage>>(responseString);
            return simplePackages.ConvertAll<string>(simplePackage => simplePackage.version);
        }

        public async Task<Package> GetPackage(string packageId, string packageVersion) 
        {
            string responseString = await _requestService.GetRequest($"{packageId}/{packageVersion}");
            return JsonConvert.DeserializeObject<Package>(responseString);
        }
    }
}
