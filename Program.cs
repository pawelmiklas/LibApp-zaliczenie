using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using LibApp.Models;
using Microsoft.Extensions.DependencyInjection;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;

namespace LibApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();
            var logger = services.GetRequiredService<ILogger<Program>>();

            try
            {
                context.Database.Migrate();
                SeedData.Initialize(services);
                logger.LogInformation("Database initialized");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while migrating data");
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
