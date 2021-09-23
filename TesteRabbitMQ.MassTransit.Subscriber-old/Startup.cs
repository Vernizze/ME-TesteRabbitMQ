using GreenPipes;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using TesteRabbitMQ.MassTransit.Subscriber.Consumers;
using TesteRabbitMQ.MassTransit.Subscriber.Settings;

namespace TesteRabbitMQ.MassTransit.Subscriber
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices() 
        {
            var services = new ServiceCollection()
                .AddMediatR(Assembly.GetAssembly(typeof(Program)))
                .AddTransient<IAddPersonConsumer, AddPersonConsumer>()
                //.GetService(out IMediator mediator)
                //.AddConsumers(mediator)
                ;




            return services;
        }

        public static IServiceCollection AddSettings(this IServiceCollection services, out AppSettings appSettings)
        {
            //appSettings = services
            //    .AddSettings<AppSettings>()
            //    .AddSettings<QueuesSettings>("QueuesSettings");
            appSettings = null;
            return services;
        }



        public static IServiceCollection AddConsumers(this IServiceCollection services, IMediator mediator/*, AppSettings appSettings*/)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost:5672/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("AddPerson", e =>
                {
                    e.PrefetchCount = 12;
                    e.ConcurrentMessageLimit = 12;
                    e.UseMessageRetry(x => x.Interval(3, TimeSpan.FromSeconds(15)));
                    e.UseRateLimit(100, TimeSpan.FromSeconds(1));
                    e.Consumer(() => new AddPersonConsumer(mediator));
                });
            });
            busControl.Start();

            return services;
        }
    }
}
