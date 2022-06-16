using HostedServiceExample.HostedServiceLogger;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HostedServiceExample.HostedService
{
    // Since there is no kind of loop or timer, this hosted service's log will only be displayed
    // once from its DoWork.
    public class SingleHostedService : IHostedService
    {
        private async Task DoWork(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return;
            await Task.Delay(1000);
            Logger.Log("SingleHostedService executed");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.Log("SingleHostedService started");
            Task.Run(() => DoWork(cancellationToken));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.Log("SingleHostedService stopped");
            return Task.CompletedTask;
        }
    }
}
