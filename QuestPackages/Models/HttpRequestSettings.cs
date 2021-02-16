using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestPackages.Models
{
    public class HttpRequestSettings : IHttpRequestSettings
    {
        public string UserAgent { get; set; }
        public string Accept { get; set; }
        public string Url { get; set; }
    }

    public interface IHttpRequestSettings
    {
        string UserAgent { get; set; }
        string Accept { get; set; }
        string Url { get; set; }
    }
}
