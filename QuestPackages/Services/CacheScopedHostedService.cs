using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace QuestPackages.Services
{
    public class CacheScopedHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider; 
        public CacheScopedHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await DoWork(cancellationToken);
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            using(var scope = _serviceProvider.CreateScope()) 
            {
                var scopedCachingService = scope.ServiceProvider.GetRequiredService<IScopedCachingService>();

                await scopedCachingService.DoWork(cancellationToken);
            }

        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
        }
    }
}
