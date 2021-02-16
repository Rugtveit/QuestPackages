using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using QuestPackages.Models;

namespace QuestPackages.Services
{
    public class RequestService
    {
        private readonly IHttpRequestSettings _httpRequestSettings;
        private readonly IHttpClientFactory _httpClientFactory;
        public RequestService(IHttpClientFactory httpClientFactory, IHttpRequestSettings requestSettings)
        {
            _httpRequestSettings = requestSettings;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetRequest(string path) 
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_httpRequestSettings.Url}{path}");
            request.Headers.Add("Accept", _httpRequestSettings.Accept);
            request.Headers.Add("User-Agent", _httpRequestSettings.UserAgent);
            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage responseMessage =  await client.SendAsync(request);
            if (!responseMessage.IsSuccessStatusCode) throw new Exception("Request failed!");
            return await responseMessage.Content.ReadAsStringAsync();
        }

    }
}
