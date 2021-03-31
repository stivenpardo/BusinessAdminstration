using BusinessAdministration.Aplication.Core.Mapper.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.Area.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Customer;
using BusinessAdministration.Aplication.Core.PeopleManagement.Customer.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Employed;
using BusinessAdministration.Aplication.Core.PeopleManagement.Employed.Services;
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
            services.TryAddTransient<IDocumentTypeService, DocumentTypeService>();
            services.TryAddTransient<IEmployedService, EmployedService>();
            services.TryAddTransient<IEmployedFacade, EmployedFacade>();
            services.TryAddTransient<ICustomerService, CustomerService>();
            services.TryAddTransient<ICustomerFacade, CustomerFacade>();

            services.ConfigureMapper();
            services.ConfigureBaseRepository(settings);
        }
    }
}
