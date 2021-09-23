namespace TesteRabbitMQ.Crosscutting.AppSettings
{
    public class AppSettings
        : BaseAppSettings
    {
        public string ElasticSearchNodeUrl { get; set; }
        public string ElasticSearchNodePort { get; set; }
        public string ElasticSearchPersonIndexName { get; set; }
        public QueuesSettings QueuesSettings { get; set; }
    }
}
