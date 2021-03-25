using BusinessAdministration.Aplication.Core.Mapper.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.Area.Services;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
            services.TryAddTransient<IAreaService, AreaService>();
            services.ConfigureMapper();
            services.ConfigureBaseRepository(settings);
        }
    }
}
