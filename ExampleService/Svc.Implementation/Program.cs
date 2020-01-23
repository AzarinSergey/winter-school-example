using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Svc.Implementation.Model;

namespace Svc.Implementation
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseUrls("http://*:15008")
                .ConfigureLogging(builder => builder.AddConsole())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<ServiceDbContext>(options =>
                options.UseSqlServer("Server=127.0.0.1,15433;Database=ExampleDb;persist security info=True;user id=sa;password=555331qQ!;"));

            services.AddScoped(p => new ServiceDbContext(p.GetService<DbContextOptions<ServiceDbContext>>()));

            services.AddHostedService<RabbitMqReceiverHostedService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            new ServiceDbContext()
                .Database.Migrate();

            app.UseMvc();
        }
    }
}
