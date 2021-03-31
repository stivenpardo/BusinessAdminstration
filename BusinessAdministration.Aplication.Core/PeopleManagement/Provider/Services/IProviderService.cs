using BusinessAdministration.Aplication.Dto.PeopleManagement.Provider;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Provider.Services
{
    public interface IProviderService
    {
        public Task<Guid> AddProvider(ProviderDto request);
        public Task<IEnumerable<ProviderDto>> GetAll();
        public bool UpdateProvider(ProviderDto request);
        public bool DeleteProvider(ProviderDto request);
    }
}