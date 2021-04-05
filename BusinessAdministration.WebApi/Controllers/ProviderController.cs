using BusinessAdministration.Aplication.Core.PeopleManagement.Provider;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Provider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly ILogger<ProviderController> _logger;
        private readonly IProviderFacade _providerFacade;

        public ProviderController(ILogger<ProviderController> logger, IProviderFacade providerFacade)
        {
            _logger = logger;
            _providerFacade = providerFacade;
        }

        [HttpPost(nameof(CreateProvider))]
        public async Task<ProviderResponseDto> CreateProvider(ProviderDto request) =>
            await _providerFacade.CreateProvider(request).ConfigureAwait(false);

        [HttpGet(nameof(GetAllProviders))]
        public async Task<IEnumerable<ProviderDto>> GetAllProviders() =>
             await _providerFacade.GetAllProviders().ConfigureAwait(false);

        [HttpPost(nameof(UpdateProvider))]
        public ProviderResponseDto UpdateProvider(ProviderDto request) =>
            _providerFacade.UpdateProvider(request);

        [HttpPost(nameof(DeleteProvider))]
        public ProviderResponseDto DeleteProvider(ProviderDto request) =>
            _providerFacade.DeleteProvider(request);
    }
}
