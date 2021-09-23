using MediatR;
using Serilog;
using System;
using System.Threading;
using TesteRabbitMQ.Crosscutting.Extensions;
using TesteRabbitMQ.UseCases.AddPerson;

namespace TesteRabbitMQ.MassTransit.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup
                .ConfigureServices()
                .GetService(out ILogger logger)
                .GetService(out IMediator mediator);

            logger.Information($"Start 'AddPerson' RabbitMQ Consumer with MassTransit at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
