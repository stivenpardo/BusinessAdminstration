using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BusinessAdministration.Aplication.Core.ExportAndImportJSON.Configuration
{
    public static class ExportAndImportJsonConfigurator
    {
        /// <summary>
        /// it can configure and use the microservice of export and import Json documents in txt 
        ///<param name="services">Service collector that has the application catalog</param>
        /// </summary>
        public static void ConfigureExportAndImportJson(this IServiceCollection services) =>
            services.TryAddTransient<IExportAndImportJson, ExportAndImportJson>();
    }
}
