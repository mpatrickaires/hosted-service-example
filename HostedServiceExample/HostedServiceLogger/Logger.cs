using System;

namespace HostedServiceExample.HostedServiceLogger
{
    public static class Logger
    {
        public static void Log(string message)
        {
            Console.WriteLine($"[{DateTime.Now}] - {message}");
        }
    }
}
