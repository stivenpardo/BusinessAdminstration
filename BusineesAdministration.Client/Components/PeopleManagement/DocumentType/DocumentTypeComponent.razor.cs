using BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.HttpClientDocumentType;
using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusineesAdministration.Client.Components.PeopleManagement.DocumentType
{
    public partial class DocumentTypeComponent : ComponentBase
    {
        [Inject]
        public IDocumentTypeHttpClient ClientDocumenType { get; set; }

        public IEnumerable<DocumentTypeDto> ListDocumentType { get; set; } = new List<DocumentTypeDto>();

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var response = await ClientDocumenType.GetAll().ConfigureAwait(false);
                ListDocumentType = response ?? new List<DocumentTypeDto>();
                await InvokeAsync(StateHasChanged).ConfigureAwait(false);
            }
            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(false);
        }
    }
}