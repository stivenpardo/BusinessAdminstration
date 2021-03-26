using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Area.Services
{
    public interface IAreaService
    {
        public Task<Guid?> AddArea(AreaRequestDto request);
        public Task<IEnumerable<AreaDto>> GetAll();
        public bool UpdateArea(AreaDto request);
        public bool DeleteArea(AreaDto request);

    }
}
