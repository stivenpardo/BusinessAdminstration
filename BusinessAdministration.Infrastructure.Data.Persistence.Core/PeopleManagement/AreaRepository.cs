using BusinessAdministration.Domain.Core.PeopleManagement.Area;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base;

namespace BusinessAdministration.Infrastructure.Data.Persistence.Core.PeopleManagement
{
    internal class AreaRepository : RepositoryBase<AreaEntity>, IAreaRepository
    {
        public AreaRepository(IContextDb unitOfWork) : base(unitOfWork) { }
    }
}
