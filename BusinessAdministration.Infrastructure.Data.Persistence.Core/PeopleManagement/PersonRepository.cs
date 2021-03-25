using BusinessAdministration.Domain.Core.PeopleManagement.Person;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base;

namespace BusinessAdministration.Infrastructure.Data.Persistence.Core.PeopleManagement
{
    internal class PersonRepository : RepositoryBase<PersonEntity>, IPersonRepository
    {
        public PersonRepository(IContextDb unitOfWork) : base(unitOfWork) { }
    }
}
