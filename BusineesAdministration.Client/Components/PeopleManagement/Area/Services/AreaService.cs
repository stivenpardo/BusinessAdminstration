using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using BusinessAdministration.Infrastructure.Transversal.GenericMethod;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusineesAdministration.Client.Components.PeopleManagement.Area.Services
{
    public class AreaService : IAreaService
    {
        private readonly IHttpGenericBaseClient _httpClient;

        public AreaService(IHttpGenericBaseClient httpClient) => _httpClient = httpClient;

        private const string UrlController = "/api/area";
        public async Task<IEnumerable<AreaDto>> GetAllAreas() =>
            await _httpClient.Get<IEnumerable<AreaDto>>("getallareas")
            .ConfigureAwait(true);
    }
}