using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Customer
{
    public interface ICustomerFacade
    {
        public Task<CustomerResponseDto> CreateCustomer(CustomerDto request);
        public Task<IEnumerable<CustomerDto>> GetAllCustomers();
        public CustomerResponseDto UpdateCustomer(CustomerDto request);
        public CustomerResponseDto DeleteCustomer(CustomerDto request);
        public Task<string> CustomerExportAll();
        public Task<IEnumerable<CustomerDto>> CustomerImportAll();

    }
}
