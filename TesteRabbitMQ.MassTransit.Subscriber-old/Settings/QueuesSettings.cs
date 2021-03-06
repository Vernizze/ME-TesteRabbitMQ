namespace TesteRabbitMQ.MassTransit.Subscriber.Settings
{
    public class QueuesSettings
        //: BaseAppSettings
    {
        public string CustomerCreatedQueueName { get; set; }
        public string PaymentCreatedQueueName { get; set; }
        public ushort PrefetchCount { get; set; }
        public ushort ConcurrencyLimit { get; set; }
        public ushort RetryNumber { get; set; }
        public ushort TimeInSecondsToRetry { get; set; }
    }
}
