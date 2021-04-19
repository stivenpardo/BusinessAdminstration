using Microsoft.Extensions.DependencyInjection;

namespace BusinessAdministration.Infrastructure.Transversal.Configurator
{
    public static class HttpClientConfigurator
    {
        public static void ConfigureHttpClientService(this IServiceCollection services, HttpClientSettings settings)
        {
            services.AddHttpClient<HttpGenericBaseClient>();
            services.Configure<HttpClientSettings>(o => o.CopyFrom(settings));
            services.AddTransient<IHttpGenericBaseClient, HttpGenericBaseClient>();
        }
    }
}
