using Microsoft.AspNetCore.Components;

namespace BusineesAdministration.Client.Components.Common.HeaderTitle
{
    public partial class HeaderTitleComponent : ComponentBase
    {
        [Parameter] public string Logo { get; set; }
        [Parameter] public string Title { get; set; }
    }
}