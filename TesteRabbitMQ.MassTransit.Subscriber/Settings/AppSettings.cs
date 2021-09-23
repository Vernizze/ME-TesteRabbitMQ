using TesteRabbitMQ.Crosscutting.AppSettings;

namespace TesteRabbitMQ.MassTransit.Subscriber.Settings
{
    public class AppSettings
        : BaseAppSettings
    {
        public QueuesSettings QueuesSettings { get; set; }
    }
}
