using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;
using System;
using System.Threading.Tasks;

namespace Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Startup>>();

                try
                {
                    var keepItCleanDbContext = services.GetRequiredService<KeepItCleanDbContext>();
                    var hostingEnvironment = services.GetRequiredService<IHostEnvironment>();
                    if (hostingEnvironment.IsDevelopment())
                    {
                        await keepItCleanDbContext.Database.EnsureDeletedAsync();
                        await keepItCleanDbContext.Database.EnsureCreatedAsync();
                    }
                    else
                    {
                        logger.LogInformation("Beginning database migration.");
                        await keepItCleanDbContext.Database.MigrateAsync();
                        logger.LogInformation("Migrated database successfully.");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred migrating the database.");
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
