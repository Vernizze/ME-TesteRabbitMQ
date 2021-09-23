using MassTransit;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using TesteRabbitMQ.MassTransit.Publisher.Settings;

namespace TesteRabbitMQ.MassTransit.Publisher
{
    public class QueueHandler
    {
        #region Variables

        private readonly QueuesSettings _queueSettings;


        #endregion

        #region Constructors 

        public QueueHandler(IOptions<QueuesSettings> queueSettingsOptions)
            => _queueSettings = queueSettingsOptions.Value;

        #endregion

        #region Methods

        public async Task Send<T>(T message, string queue)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(_queueSettings.UriServer), h =>
                {
                    h.Username(_queueSettings.Login);
                    h.Password(_queueSettings.Pwd);
                });
            });

            bus.Start();

            Task<ISendEndpoint> sendEndpointTask = bus.GetSendEndpoint(new Uri($"{_queueSettings.UriServer}/{queue}"));
            ISendEndpoint sendEndpoint = sendEndpointTask.Result;

            await sendEndpoint.Send(message);
        }

        #endregion
    }
}
