using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Area.Services
{
    public interface IAreaService
    {
        public Task<IEnumerable<AreaDto>> GetAll(AreaRequestDto request);
        public Task<Guid?> AddArea(AreaRequestDto request);
        public bool UpdateArea(AreaDto request);
        public bool DeleteArea(AreaDto request);

    }
}
