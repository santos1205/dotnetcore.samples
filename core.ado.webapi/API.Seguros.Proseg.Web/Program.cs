using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using API.Seguros.Proseg.Infrastructure.Data;
using System;

namespace API.Seguros.Proseg.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();

            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<MultCalcSegContext>();
                    context.Database.EnsureCreated();
                    DbInitializer.Initializer(services);

                    var contextMS = services.GetRequiredService<MultiSegurosContext>();
                    context.Database.EnsureCreated();
                    DbInitializer.Initializer(services);

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
        }

        public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args)
                                                                     .UseStartup<Startup>()
                                                                     .Build();
    }
}
