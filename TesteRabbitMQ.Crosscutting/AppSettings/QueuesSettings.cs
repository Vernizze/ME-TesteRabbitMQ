namespace TesteRabbitMQ.Crosscutting.AppSettings
{
    public class QueuesSettings
        : BaseAppSettings
    {
        public string UriServer { get; set; }
        public string Login { get; set; }
        public string Pwd { get; set; }
        public string AddPersonQueueName { get; set; }
        public ushort PrefetchCount { get; set; }
        public ushort ConcurrencyLimit { get; set; }
        public ushort RetryNumber { get; set; }
        public ushort TimeInSecondsToRetry { get; set; }
        public ushort TimeInSecondsToRateLimit { get; set; }
    }
}
