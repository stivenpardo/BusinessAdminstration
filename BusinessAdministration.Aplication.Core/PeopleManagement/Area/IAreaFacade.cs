using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Area
{
    public interface IAreaFacade
    {
        public Task<AreaResponseDto> CreateArea(AreaRequestDto request);
        public Task<IEnumerable<AreaDto>> GetAllAreas();
        public AreaResponseDto UpdateArea(AreaDto request);
        public AreaResponseDto DeleteArea(AreaDto request);
        public Task<string> AreaExportAll();
        public Task<IEnumerable<AreaDto>> AreaImportAll();
    }
}
