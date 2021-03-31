using BusinessAdministration.Aplication.Core.PeopleManagement.Customer.Services;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Customer
{
    public class CustomerFacade : ICustomerFacade
    {
        private readonly ICustomerService _customerSvc;
        public CustomerFacade(ICustomerService customerSvc) =>
            _customerSvc = customerSvc;

        public async Task<CustomerResponseDto> CreateCustomer(CustomerDto request)
        {
            var response = await _customerSvc.AddCustomer(request).ConfigureAwait(false) != default;
            return new CustomerResponseDto
            {
                StatusCode = response ? HttpStatusCode.OK : HttpStatusCode.Unauthorized,
                StatusDescription = response ? "Inserted customer " : "Not inserted customer",
            };
        }

        public CustomerResponseDto DeleteCustomer(CustomerDto request)
        {
            var response = _customerSvc.DeleteCustomer(request);
            return new CustomerResponseDto
            {
                StatusCode = response ? HttpStatusCode.OK : HttpStatusCode.Unauthorized,
                StatusDescription = response ? "Inserted customer " : "Not inserted customer",
            };
        }
        public Task<IEnumerable<CustomerDto>> GetAllCustomers() => _customerSvc.GetAll();
        public CustomerResponseDto UpdateCustomer(CustomerDto request)
        {
            var response = _customerSvc.UpdateCustomer(request);
            return new CustomerResponseDto
            {
                StatusCode = response ? HttpStatusCode.OK : HttpStatusCode.Unauthorized,
                StatusDescription = response ? "Inserted customer" : "Not inserted customer",
            };
        }
        public async Task<string> CustomerExportAll() =>
            await _customerSvc.ExportAll().ConfigureAwait(false);
        public async Task<IEnumerable<CustomerDto>> CustomerImportAll() => 
            await _customerSvc.ImportAll().ConfigureAwait(false);
    }
}
