using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Employed
{
    public interface IEmployedFacade
    {
        public Task<EmployedResponseDto> CreateEmployed(EmployedDto request);
        public Task<IEnumerable<EmployedDto>> GetAllEmployees();
        public EmployedResponseDto UpdateEmployed(EmployedDto request);
        public EmployedResponseDto DeleteEmployed(EmployedRequestDto request);
    }
}
