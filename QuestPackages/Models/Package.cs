using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestPackages.Models
{
    public class SimplePackage
    {
        public string id { get; set; }
        public string version { get; set; }
    }
    public class Package
    {
        public Config config { get; set; }
        public List<RestoredDependency> restoredDependencies { get; set; }

    }
    public class AdditionalData
    {
        public string branchName { get; set; }
        public string soLink { get; set; }
        public string debugSoLink { get; set; }
    }

    public class Info
    {
        public string name { get; set; }
        public string id { get; set; }
        public string version { get; set; }
        public string url { get; set; }
        public AdditionalData additionalData { get; set; }
    }

    public class Config
    {
        public string sharedDir { get; set; }
        public string dependenciesDir { get; set; }
        public Info info { get; set; }
        public List<Dependency> dependencies { get; set; }
        public AdditionalData additionalData { get; set; }
    }

    public class RestoredDependency
    {
        public Dependency dependency { get; set; }
        public string version { get; set; }
    }

    public class Dependency
    {
        public string Id { get; set; }
        public string VersionRange { get; set; }
    }

}
