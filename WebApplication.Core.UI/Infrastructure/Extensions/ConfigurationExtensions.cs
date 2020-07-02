using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace WebApplication.Core.UI.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {

        public static string GetPostgresConnectionString(this IConfiguration config, string connectionString)
        {
            var connUrl = connectionString.Replace("postgres://", string.Empty);
            var pgUserPass = connUrl.Split("@")[0];
            var pgHostPortDb = connUrl.Split("@")[1];
            var pgHostPort = pgHostPortDb.Split("/")[0];
            var pgDb = pgHostPortDb.Split("/")[1];
            var pgUser = pgUserPass.Split(":")[0];
            var pgPass = pgUserPass.Split(":")[1];
            var pgHost = pgHostPort.Split(":")[0];
            var pgPort = pgHostPort.Split(":")[1];

            return $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb}";
        }

        public static string GetConnectionString(this IConfiguration config, IWebHostEnvironment environment)
        {
            var connectionString = config.GetConnectionString("WEBAPPLICATION_DB");

            if (environment.IsProduction())
            {
                var connUrl = System.Environment.GetEnvironmentVariable("DATABASE_URL");
                connectionString = config.GetPostgresConnectionString(connUrl);
            }

            return connectionString;
        }

    }
}
