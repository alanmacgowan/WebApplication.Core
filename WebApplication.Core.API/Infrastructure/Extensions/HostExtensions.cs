using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System;

namespace WebApplication.Core.API.Infrastructure.Extensions
{
    public static class HostExtensions
    {

        public static IHost MigrateDatabase<TContext>(this IHost webHost) where TContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();
                try
                {
                    logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name} - {context.Database.GetDbConnection().ConnectionString}");

                    var retry = Policy.Handle<SqlException>()
                                      .WaitAndRetry(new TimeSpan[]
                                      {
                                        TimeSpan.FromSeconds(60),
                                        TimeSpan.FromSeconds(60),
                                        TimeSpan.FromSeconds(120),
                                      });

                    retry.Execute(() => context.Database.Migrate());

                    logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
                }
            }
            return webHost;
        }

    }
}
