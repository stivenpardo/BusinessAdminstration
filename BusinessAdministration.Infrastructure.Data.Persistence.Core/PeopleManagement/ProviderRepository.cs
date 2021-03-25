using BusinessAdministration.Domain.Core.PeopleManagement.Provider;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base;

namespace BusinessAdministration.Infrastructure.Data.Persistence.Core.PeopleManagement
{
    internal class ProviderRepository : RepositoryBase<ProviderEntity>, IProviderRepository
    {
        public ProviderRepository(IContextDb unitOfWork) : base(unitOfWork) { }
    }
}
