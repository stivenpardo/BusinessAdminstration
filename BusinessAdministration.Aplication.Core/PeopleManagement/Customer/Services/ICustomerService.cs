using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Customer.Services
{
    public interface ICustomerService
    {
        public Task<Guid> AddCustomer(CustomerDto request);
        public Task<IEnumerable<CustomerDto>> GetAll();
        public bool UpdateCustomer(CustomerDto request);
        public bool DeleteCustomer(CustomerDto request);
        public Task<string> ExportAll();
        public Task<IEnumerable<CustomerDto>> ImportAll();
    }
}