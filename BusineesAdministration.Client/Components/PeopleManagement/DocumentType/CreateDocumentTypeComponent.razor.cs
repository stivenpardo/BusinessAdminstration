using BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.HttpClientDocumentType;
using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BusineesAdministration.Client.Components.PeopleManagement.DocumentType
{
    public partial class CreateDocumentTypeComponent : ComponentBase
    {
        [Inject]
        public IDocumentTypeHttpClient ClientDocumentType { get; set; }

        [Inject]
        private IJSRuntime Js { get; set; }

        private DocumentTypeDto ObjDocumentType { get; set; } = new();

        private async Task Success()
        {
            var response = await ClientDocumentType.Create(ObjDocumentType).ConfigureAwait(false);
            if (string.Equals(response.StatusCode.ToString(), "ok", StringComparison.OrdinalIgnoreCase))
            {
                await Js.InvokeAsync<object>("alert", "Registro exitoso").ConfigureAwait(false);
                await InvokeAsync(StateHasChanged).ConfigureAwait(false);
            }
        }
    }
}