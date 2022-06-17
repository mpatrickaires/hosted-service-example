using HostedServiceExample.HostedService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HostedServiceExample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Notice, through the Console, how the hosted services will be executed before the
            // application has even started. Also, the order in which they are added with the 
            // AddHostedService will be the order in which each one is executed.
            services.AddHostedService<SingleHostedService>();
            services.AddHostedService<LoopHostedService>();
            services.AddHostedService<TimedHostedService>();
            services.AddHostedService<BackgroundServiceHostedService>();

            // Also, no matter how many times we call AddHostedService for a class, it will
            // be created only once, having a Singleton behavior.
            services.AddHostedService<LoopHostedService>();
            services.AddHostedService<LoopHostedService>();
            services.AddHostedService<LoopHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
