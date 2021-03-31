using BusinessAdministration.Aplication.Core.PeopleManagement.Customer;
using BusinessAdministration.Aplication.Core.PeopleManagement.Provider.Services;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Provider;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Provider
{
    public class ProviderFacade : IProviderFacade
    {
        private readonly IProviderService _providerSvc;
        public ProviderFacade(IProviderService providerSvc) =>
            _providerSvc = providerSvc;

        public async Task<ProviderResponseDto> CreateProvider(ProviderDto request)
        {
            var response = await _providerSvc.AddProvider(request).ConfigureAwait(false) != default;
            return new ProviderResponseDto
            {
                StatusCode = response ? HttpStatusCode.OK : HttpStatusCode.Unauthorized,
                StatusDescription = response ? "Inserted provider " : "Not inserted provider",
            };
        }

        public ProviderResponseDto DeleteProvider(ProviderDto request)
        {
            var response = _providerSvc.DeleteProvider(request);
            return new ProviderResponseDto
            {
                StatusCode = response ? HttpStatusCode.OK : HttpStatusCode.Unauthorized,
                StatusDescription = response ? "Inserted provider " : "Not inserted provider",
            };
        }
        public Task<IEnumerable<ProviderDto>> GetAllProviders() => _providerSvc.GetAll();
        public ProviderResponseDto UpdateProvider(ProviderDto request)
        {
            var response = _providerSvc.UpdateProvider(request);
            return new ProviderResponseDto
            {
                StatusCode = response ? HttpStatusCode.OK : HttpStatusCode.Unauthorized,
                StatusDescription = response ? "Inserted provider" : "Not inserted provider",
            };
        }
    }
}
