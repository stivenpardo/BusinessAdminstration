using BusinessAdministration.Aplication.Core.PeopleManagement.Area.Services;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Area
{
    public class AreaFacade : IAreaFacade
    {
        private readonly IAreaService _areaSvc;
        public AreaFacade(IAreaService areaSvc) =>
            _areaSvc = areaSvc;

        public async Task<AreaResponseDto> CreateArea(AreaRequestDto request)
        {
            var response = await _areaSvc.AddArea(request).ConfigureAwait(false) != default;
            return new AreaResponseDto
            {
                StatusCode = response ? HttpStatusCode.OK : HttpStatusCode.Unauthorized,
                StatusDescription = response ? "Inserted area " : "Not inserted area",
            };
        }

        public AreaResponseDto DeleteArea(AreaDto request)
        {
            var response = _areaSvc.DeleteArea(request);
            return new AreaResponseDto
            {
                StatusCode = response ? HttpStatusCode.OK : HttpStatusCode.Unauthorized,
                StatusDescription = response ? "Inserted area " : "Not inserted area",
            };
        }
        public async Task<IEnumerable<AreaDto>> GetAllAreas() => await _areaSvc.GetAll().ConfigureAwait(false);
        public AreaResponseDto UpdateArea(AreaDto request)
        {
            var response = _areaSvc.UpdateArea(request);
            return new AreaResponseDto
            {
                StatusCode = response ? HttpStatusCode.OK : HttpStatusCode.Unauthorized,
                StatusDescription = response ? "Inserted area" : "Not inserted area",
            };
        }
        public async Task<string> AreaExportAll() =>
            throw new Exception();
        //await _areaSvc.ExportAll().ConfigureAwait(false);
        public async Task<IEnumerable<AreaDto>> AreaImportAll() =>
            //await _areaSvc.ImportAll().ConfigureAwait(false);
            throw new Exception();
    }
}
