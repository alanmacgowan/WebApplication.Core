using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WebApplication.Core.API.Infrastructure.Extensions;
using WebApplication.Core.Data;

namespace WebApplication.Core.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().MigrateDatabase<WebApplicationContext>().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
