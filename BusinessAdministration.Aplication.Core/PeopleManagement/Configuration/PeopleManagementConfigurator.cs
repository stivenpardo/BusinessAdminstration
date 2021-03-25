using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Configuration
{
    public static class PeopleManagementConfigurator
    {
        /// <summary>
        /// it can configure and use the microservice of people management 
        ///<param name="services">Service collector that has the application catalog</param>
        /// </summary>
        public static void ConfigurePeopleManagementService(this IServiceCollection services, DbSettings settings)
        {
            //services.ConfigureMapper();
            services.ConfigureBaseRepository(settings);
        }
    }
}
