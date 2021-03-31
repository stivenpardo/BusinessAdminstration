using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Provider;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Provider
{
    public interface IProviderFacade
    {
        public Task<ProviderResponseDto> CreateProvider(ProviderDto request);
        public Task<IEnumerable<ProviderDto>> GetAllProviders();
        public ProviderResponseDto UpdateProvider(ProviderDto request);
        public ProviderResponseDto DeleteProvider(ProviderDto request);
    }
}
