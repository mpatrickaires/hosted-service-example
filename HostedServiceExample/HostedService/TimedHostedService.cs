using HostedServiceExample.HostedServiceLogger;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostedServiceExample.HostedService
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private Timer _timer;

        private void DoWork(object state)
        {
            Logger.Log("TimedHostedService executed");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.Log("TimedHostedService started");
            // Here, we don't need a while loop: the Timer will execute the DoWork every one second
            // in a thread from the thread pool.
            _timer = new Timer(DoWork, null, 1000, 1000);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.Log("TimedHostedService stopped");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Logger.Log("TimedHostedService disposed");
            _timer?.Dispose();
        }
    }
}
