using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using TesteRabbitMQ.Crosscutting.Extensions;
using TesteRabbitMQ.MassTransit.Publisher.Settings;
using TesteRabbitMQ.UseCases.GeneratePerson;

namespace TesteRabbitMQ.MassTransit.Publisher
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection()
                .AddMediatR(Assembly.GetAssembly(typeof(GeneratePerson)))                
                .AddSettings(out AppSettings appSettings)
                .AddScoped<QueueHandler>()
                .AddLogs()
                ;

            return services;
        }

        public static IServiceCollection AddSettings(this IServiceCollection services, out AppSettings appSettings)
        {
            services
                .AddSettings<AppSettings>()
                .AddSettings<QueuesSettings>("QueuesSettings")
                .GetService(out IOptions<AppSettings> appSettingsOptions)
                ;

            appSettings = appSettingsOptions.Value;

            return services;
        }
    }
}
