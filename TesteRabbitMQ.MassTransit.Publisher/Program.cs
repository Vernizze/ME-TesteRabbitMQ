using MediatR;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Threading;
using System.Timers;
using TesteRabbitMQ.Crosscutting.AppSettings;
using TesteRabbitMQ.Crosscutting.Extensions;
using TesteRabbitMQ.UseCases.GeneratePerson;

namespace TesteRabbitMQ.MassTransit.Publisher
{
    class Program
    {
        private static System.Timers.Timer aTimer;
        private static IOptions<AppSettings> _appSettingsOptions;
        private static ILogger _logger;
        private static IMediator _mediator;
        private static QueueHandler _queueHandler;

        static void Main(string[] args)
        {
            Startup.ConfigureServices()
                .GetService(out _mediator)                
                .GetService(out _appSettingsOptions)
                .GetService(out _queueHandler)
                .GetService(out _logger);

            _logger.Information($"Start 'AddPerson' RabbitMQ Producer with MassTransit at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");

            SetTimer();

            Thread.Sleep(Timeout.Infinite);
        }

        private static void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(10000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            _logger.Information($"Start generate new ten Persons...");

            for (int i = 0; i < 2; i++)
            {
                var person = _mediator.Send(new GeneratePerson.Model.Input()).GetAwaiter().GetResult();

                _queueHandler.Send(person.Input, _appSettingsOptions.Value.QueuesSettings.AddPersonQueueName).GetAwaiter().GetResult();
            }

            _logger.Information($"End generate Persons...");
        }
    }
}
