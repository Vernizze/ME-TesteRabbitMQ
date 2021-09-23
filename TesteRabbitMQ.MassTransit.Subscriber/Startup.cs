using GreenPipes;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;
using TesteRabbitMQ.Crosscutting.AppSettings;
using TesteRabbitMQ.Crosscutting.Extensions;
using TesteRabbitMQ.MassTransit.Subscriber.Consumers;
using TesteRabbitMQ.UseCases.GeneratePerson;

namespace TesteRabbitMQ.MassTransit.Subscriber
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices() 
        {
            var services = new ServiceCollection()
                .AddMediatR(Assembly.GetAssembly(typeof(GeneratePerson)))
                .AddScoped<IAddPersonConsumer, AddPersonConsumer>()
                .AddSettings(out AppSettings appSettings)
                .AddLogs()
                .AddConsumers(appSettings)                
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

        public static IServiceCollection AddConsumers(this IServiceCollection services, AppSettings appSettings)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(appSettings.QueuesSettings.UriServer), h =>
                {
                    h.Username(appSettings.QueuesSettings.Login);
                    h.Password(appSettings.QueuesSettings.Pwd);
                });

                services.GetService(out IAddPersonConsumer addPersonConsumer);

                cfg.ReceiveEndpoint(appSettings.QueuesSettings.AddPersonQueueName, e =>
                {
                    e.PrefetchCount = appSettings.QueuesSettings.PrefetchCount;
                    e.ConcurrentMessageLimit = appSettings.QueuesSettings.ConcurrencyLimit;
                    e.UseMessageRetry(x => x.Interval(appSettings.QueuesSettings.RetryNumber, TimeSpan.FromSeconds(appSettings.QueuesSettings.TimeInSecondsToRetry)));
                    e.UseRateLimit(100, TimeSpan.FromSeconds(appSettings.QueuesSettings.TimeInSecondsToRateLimit));
                    e.Consumer(() => addPersonConsumer as AddPersonConsumer);
                });
            });

            busControl.Start();

            return services;
        }
    }
}
