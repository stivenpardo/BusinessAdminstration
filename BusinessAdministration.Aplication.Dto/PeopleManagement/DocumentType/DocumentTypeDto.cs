using BusinessAdministration.Aplication.Dto.Base;
using System;

namespace BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType
{
    public class DocumentTypeDto : DataTransferObject
    {
        public Guid DocumentTypeId { get; set; }
        public string DocumentType { get; set; }
    }
}
