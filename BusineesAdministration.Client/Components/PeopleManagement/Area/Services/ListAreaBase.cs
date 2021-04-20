using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusineesAdministration.Client.Components.PeopleManagement.Area.Services
{
    public class ListAreaBase : ComponentBase
    {
        [Inject]
        public IAreaService AreaService { get; set; }
        public IEnumerable<AreaDto> Areas { get; set; }
        protected async override Task OnInitializedAsync()
        {
            Areas = (await AreaService.GetAllAreas().ConfigureAwait(true)).ToList();
        }

    }
}
