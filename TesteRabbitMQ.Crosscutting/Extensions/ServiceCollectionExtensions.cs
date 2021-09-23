using Microsoft.Extensions.DependencyInjection;
using System;

namespace TesteRabbitMQ.Crosscutting.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection GetService<T>(this IServiceCollection services, out T service)
        {
            service = services.BuildServiceProvider().GetService<T>();

            return services;
        }
    }
}
