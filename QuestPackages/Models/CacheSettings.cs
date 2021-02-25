using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestPackages.Models
{
    public class CacheSettings : ICacheSettings
    {
        public Interval CachingInterval { get; set; }
        public bool CachingEnabled { get; set; }
    }

    public interface ICacheSettings
    {
        Interval CachingInterval { get; set; }
        bool CachingEnabled { get; set; }
    }

    public class Interval
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
    }
}
