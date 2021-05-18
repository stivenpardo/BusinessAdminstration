using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using Microsoft.AspNetCore.Components;

namespace BusineesAdministration.Client.Components.PeopleManagement.DocumentType
{
    public partial class FormRegister : ComponentBase
    {
        [Parameter]
        public string NameField1 { get; set; }

        [Parameter]
        public string NameButton { get; set; }

        [Parameter]
        public DocumentTypeDto ObjDocumentType { get; set; } = new();

        public void Success() { }
    }
}
