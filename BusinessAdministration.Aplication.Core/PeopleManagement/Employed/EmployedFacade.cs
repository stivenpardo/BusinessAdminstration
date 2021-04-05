using BusinessAdministration.Aplication.Core.PeopleManagement.Employed.Services;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Employed
{
    public class EmployedFacade : IEmployedFacade
    {
        private readonly IEmployedService _employedSvc;
        public EmployedFacade(IEmployedService employedSvc) =>
            _employedSvc = employedSvc;
        
        public async Task<EmployedResponseDto> CreateEmployed(EmployedDto request)
        {
            var response = await _employedSvc.AddEmployed(request).ConfigureAwait(false) != default;
            return new EmployedResponseDto
            {
                StatusCode = response ? HttpStatusCode.OK : HttpStatusCode.Unauthorized,
                StatusDescription = response ? "Inserted employee " : "Not inserted employee",
            };
        }

        public EmployedResponseDto DeleteEmployed(EmployedRequestDto request)
        {
            var response = _employedSvc.DeleteEmployed(request);
            return new EmployedResponseDto
            {
                StatusCode = response ? HttpStatusCode.OK : HttpStatusCode.Unauthorized,
                StatusDescription = response ? "Inserted employee " : "Not inserted employee",
            };
        }
        public async Task<IEnumerable<EmployedDto>> GetAllEmployees() => await _employedSvc.GetAll().ConfigureAwait(false);
        public EmployedResponseDto UpdateEmployed(EmployedDto request)
        {
            var response = _employedSvc.UpdateEmployed(request);
            return new EmployedResponseDto
            {
                StatusCode = response ? HttpStatusCode.OK : HttpStatusCode.Unauthorized,
                StatusDescription = response ? "Inserted employee " : "Not inserted employee",
            };
        }
    }
}
