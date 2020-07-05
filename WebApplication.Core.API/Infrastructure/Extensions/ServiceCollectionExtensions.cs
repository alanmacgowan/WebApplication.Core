using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebApplication.Core.API.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            return collection;
        }
    }
}
