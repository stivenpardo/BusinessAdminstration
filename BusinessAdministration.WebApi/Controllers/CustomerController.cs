using BusinessAdministration.Aplication.Core.PeopleManagement.Customer;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerFacade _customerFacade;

        public CustomerController(ILogger<CustomerController> logger, ICustomerFacade customerFacade)
        {
            _logger = logger;
            _customerFacade = customerFacade;
        }

        [HttpPost(nameof(CreateCustomer))]
        public async Task<CustomerResponseDto> CreateCustomer(CustomerDto request) =>
            await _customerFacade.CreateCustomer(request).ConfigureAwait(false);

        [HttpGet(nameof(GetAllCustomers))]
        public async Task<IEnumerable<CustomerDto>> GetAllCustomers() =>
             await _customerFacade.GetAllCustomers().ConfigureAwait(false);

        [HttpPost(nameof(UpdateCustomer))]
        public CustomerResponseDto UpdateCustomer(CustomerDto request) =>
            _customerFacade.UpdateCustomer(request);

        [HttpPost(nameof(DeleteCustomer))]
        public CustomerResponseDto DeleteCustomer(CustomerDto request) =>
            _customerFacade.DeleteCustomer(request);
    }
}
