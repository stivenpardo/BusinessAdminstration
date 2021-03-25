using BusinessAdministration.Domain.Core.PeopleManagement.Customer;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base;

namespace BusinessAdministration.Infrastructure.Data.Persistence.Core.PeopleManagement
{
    internal class CustomerRepository : RepositoryBase<CustomerEntity>, ICustomerRepository
    {
        public CustomerRepository(IContextDb unitOfWork) : base(unitOfWork) { }
    }
}
