using BusinessAdministration.Aplication.Core.PeopleManagement.Employed;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployedController : ControllerBase
    {
        private readonly ILogger<EmployedController> _logger;
        private readonly IEmployedFacade _employedFacade;

        public EmployedController(ILogger<EmployedController> logger, IEmployedFacade employedFacade)
        {
            _logger = logger;
            _employedFacade = employedFacade;
        }

        [HttpPost(nameof(CreateEmployed))]
        public async Task<EmployedResponseDto> CreateEmployed(EmployedDto request) =>
            await _employedFacade.CreateEmployed(request).ConfigureAwait(false);

        [HttpGet]
        public async Task<IEnumerable<EmployedDto>> GetAllEmployees() =>
            await _employedFacade.GetAllEmployees().ConfigureAwait(false);
    }
}
