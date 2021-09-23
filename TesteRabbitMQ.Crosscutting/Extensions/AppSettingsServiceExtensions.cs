using Microsoft.Extensions.DependencyInjection;
using TesteRabbitMQ.Crosscutting.AppSettings;

namespace TesteRabbitMQ.Crosscutting.Extensions
{
    public static class AppSettingsServiceExtensions
    {
        public static IServiceCollection AddSettings<T>(this IServiceCollection services, string jsonAttributeName)
             where T : BaseAppSettings
            => services
                    .Configure<T>(ConfigurationFactory.Instance.Configuration.GetSection(jsonAttributeName));

        public static IServiceCollection AddSettings<T>(this IServiceCollection services)
            where T : BaseAppSettings
            => services
                    .Configure<T>(ConfigurationFactory.Instance.Configuration);
    }
}
