using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Employed.Services
{
    public interface IEmployedService
    {
        public Task<Guid?> AddEmployed(EmployedDto request);
        public Task<IEnumerable<EmployedDto>> GetAll();
        public bool UpdateEmployed(EmployedDto request);
        public bool DeleteEmployed(EmployedRequestDto request);
    }
}
