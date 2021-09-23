using TesteRabbitMQ.Crosscutting.AppSettings;

namespace TesteRabbitMQ.MassTransit.Publisher.Settings
{
    public class AppSettings
        : BaseAppSettings
    {
        public QueuesSettings QueuesSettings { get; set; }
    }
}
