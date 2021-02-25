using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using QuestPackages.Models;
namespace QuestPackages.Services
{
    internal interface IScopedCachingService
    {
        Task DoWork(CancellationToken stoppingToken);
    }

   
    public class ScopedCachingService : IScopedCachingService
    {
        private readonly CachingService _cachingService;
        private readonly ICacheSettings _cacheSettings; 
        private Timer _timer;
        private readonly TimeSpan _interval;
        public ScopedCachingService(CachingService cachingService, ICacheSettings cacheSettings)
        {
            _cachingService = cachingService;
            _cacheSettings = cacheSettings;
            _interval = new TimeSpan(_cacheSettings.CachingInterval.Hours,
                                     _cacheSettings.CachingInterval.Minutes,
                                     _cacheSettings.CachingInterval.Seconds);
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            if (!_cacheSettings.CachingEnabled) return;
            while (!stoppingToken.IsCancellationRequested)
            {
                await _cachingService.UpdateCache();
                await Task.Delay(_interval, stoppingToken);
            }
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}
