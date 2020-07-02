using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApplication.Core.UI.Infrastructure.Middleware;

namespace WebApplication.Core.UI.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder app, bool isDevelopment)
        {
            if (isDevelopment)
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
            }
            return app;
        }

        public static void UseCustomExceptionMiddleware(this IApplicationBuilder app, IWebHostEnvironment environment, ILogger logger)
        {
            if (environment.IsProduction())
            {
                app.UseMiddleware<ExceptionMiddleware>(logger);
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }
        }

    }
}
