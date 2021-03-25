using BusinessAdministration.Domain.Core.PeopleManagement.Area;
using BusinessAdministration.Domain.Core.PeopleManagement.Customer;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using BusinessAdministration.Domain.Core.PeopleManagement.Employed;
using BusinessAdministration.Domain.Core.PeopleManagement.Provider;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.PeopleManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration
{
    public static class RepositoryConfigurator
    {
        public static void ConfigureBaseRepository(this IServiceCollection services, DbSettings settings)
        {
            services.TryAddTransient<IAreaRepository, AreaRepository>();
            services.TryAddTransient<ICustomerRepository, CustomerRepository>();
            services.TryAddTransient<IDocumentTypeRepository, DocumentTypeRepository>();
            services.TryAddTransient<IEmployedRepository, EmployedRepository>();
            services.TryAddTransient<IProviderRepository, ProviderRepository>();
            
            services.ConfigureContext(settings);
        }

        public static void ConfigureContext(this IServiceCollection services, DbSettings settings)
        {
            services.Configure<DbSettings>(o => o.CopyFrom(settings));
            services.TryAddScoped<IContextDb, ContextDb>();
        }
    }
}
