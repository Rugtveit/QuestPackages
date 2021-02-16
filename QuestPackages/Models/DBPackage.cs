using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestPackages.Models
{
    public class DBPackage
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public DBDependency[] Dependencies { get; set; }
        public string Url { get; set; }
        public string DownloadUrl { get; set; }
        public string[] Versions { get; set; } 
    }

    public class DBDependency
    {
        public string Id { get; set; }
        public string VersionRange { get; set; }
    }
}
