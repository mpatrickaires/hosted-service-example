using HostedServiceExample.HostedServiceLogger;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HostedServiceExample.HostedService
{
    public class LoopHostedService : IHostedService
    {
        private async Task DoWork(CancellationToken cancellationToken)
        {
            // To do the test of the CancellationToken here, all we need to do is block the thread
            // with Thread.Sleep, and then stop the application through the console with "Ctrl+C"
            // before the blocked thread returns from the Thread.Sleep. This will mark the
            // CancellationToken as true and thus will represent that the StartAsync was aborted.
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000);
                Logger.Log($"LoopHostedService executed");
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.Log("LoopHostedService started");
            // If DoWork is called in the following way, the application will never startç
            // await DoWork(cancellationToken);

            // The following way will not block the application from starting, but the compiler will
            // give a warning due to calling the method without the "await" keywordç
            // DoWork(cancellationToken);

            // The following way will not block the application nor generate warnings, but will
            // consume a thread from the thread pool to execute.
            Task.Run(() => DoWork(cancellationToken));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Thread.Sleep(6000);
            // If the Thread.Sleep(6000) is uncommented, the CancellationToken will be set to True
            // due to the timeout of five seconds being reached, which will also throw an Exception.
            // However, something is curious: the CancellationToken at the DoWork method will not be
            // set to True. Apparently, the purpose of its CancellationToken is just to abort the
            // starting process, without being influented by the StopAsync's CancellationToken.
            // Reference: https://stackoverflow.com/a/61125561/19109739

            Logger.Log($"LoopHostedService stopped");
            return Task.CompletedTask;
        }
    }
}
