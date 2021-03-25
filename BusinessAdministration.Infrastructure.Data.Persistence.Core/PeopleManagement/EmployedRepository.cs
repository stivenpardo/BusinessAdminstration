using BusinessAdministration.Domain.Core.PeopleManagement.Employed;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base;

namespace BusinessAdministration.Infrastructure.Data.Persistence.Core.PeopleManagement
{
    internal class EmployedRepository : RepositoryBase<EmployedEntity>, IEmployedRepository
    {
        public EmployedRepository(IContextDb unitOfWork) : base(unitOfWork) { }
    }
}
