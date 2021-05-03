using BusinessAdministration.Aplication.Core.PeopleManagement.Area.HttpClientArea;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusineesAdministration.Client.Components.PeopleManagement.Area
{
    public partial class CreateAreaComponent
    {
        [Inject]
        public IAreaClienteHttp ClienteHttp { get; set; }
        public IEnumerable<AreaDto> Areas { get; set; } = new List<AreaDto>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var response = await ClienteHttp.GetAll().ConfigureAwait(false);
                Areas = response ?? new List<AreaDto>();
                await InvokeAsync(StateHasChanged).ConfigureAwait(false);
            }
            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
