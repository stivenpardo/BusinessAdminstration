using BusinessAdministration.Aplication.Core.PeopleManagement.Area;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly ILogger<AreaController> _logger;
        private readonly IAreaFacade _areaFacade;

        public AreaController(ILogger<AreaController> logger, IAreaFacade areaFacade)
        {
            _logger = logger;
            _areaFacade = areaFacade;
        }

        [HttpPost(nameof(CreateArea))]
        public async Task<AreaResponseDto> CreateArea(AreaRequestDto request) =>
            await _areaFacade.CreateArea(request).ConfigureAwait(false);

        [HttpGet(nameof(GetAllAreas))]
        public async Task<IEnumerable<AreaDto>> GetAllAreas() =>
             await _areaFacade.GetAllAreas().ConfigureAwait(false);

        [HttpPost(nameof(UpdateArea))]
        public AreaResponseDto UpdateArea(AreaDto request) =>
            _areaFacade.UpdateArea(request);

        [HttpPost(nameof(DeleteArea))]
        public AreaResponseDto DeleteArea(AreaDto request) =>
            _areaFacade.DeleteArea(request);
    }
}
