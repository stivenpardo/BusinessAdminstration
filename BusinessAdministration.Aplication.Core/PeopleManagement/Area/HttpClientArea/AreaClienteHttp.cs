using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using BusinessAdministration.Infrastructure.Transversal;
using BusinessAdministration.Infrastructure.Transversal.Configurator;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Area.HttpClientArea
{
    public class AreaClienteHttp : HttpClientGenericBase<AreaDto>, IAreaClienteHttp
    {
        public AreaClienteHttp(HttpClient client, IOptions<HttpClientSettings> settings) : base(client, settings)
        {
        }

        protected override string Controller { get => "/Area"; }

        public async Task<IEnumerable<AreaDto>> GetAll() => await Get("GetAllAreas").ConfigureAwait(false);

    }
}
