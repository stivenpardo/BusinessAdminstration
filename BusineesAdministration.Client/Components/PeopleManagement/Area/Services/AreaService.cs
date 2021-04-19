using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using MatBlazor;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BusineesAdministration.Client.Components.PeopleManagement.Area.Services
{
    public class AreaService : IAreaService
    {
        private readonly HttpClient _httpClient;

        public AreaService(HttpClient httpClient) => _httpClient = httpClient;

        private const string UrlController = "/api/area";
        public async Task<IEnumerable<AreaDto>> AreaGetAll() =>
            await _httpClient.GetJsonAsync<AreaDto[]>($"{UrlController}/getallareas")
            .ConfigureAwait(false);
    }
}