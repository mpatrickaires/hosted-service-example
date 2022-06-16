using HostedServiceExample.HostedServiceLogger;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HostedServiceExample.HostedService
{
    // The BackgroundService greatly reduces the necessary code by having a default implementation
    // of the HostedService that we do most of the time; basically, it already handles the StartAsync
    // and StopAsync of the IHostedService interface, requesting only the implementation of the
    // abstract ExecuteAsync method, which should contain the executing task.
    public class BackgroundServiceHostedService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000);
                Logger.Log($"BackgroundServiceHostedService executed");
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.Log("BackgroundServiceHostedService started");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.Log("BackgroundServiceHostedService stopped");
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            Logger.Log("BackgroundServiceHostedService disposed");
            base.Dispose();
        }
    }
}
