using BusinessAdministration.Aplication.Dto.Base;
using System;

namespace BusinessAdministration.Aplication.Dto.PeopleManagement
{
    public class PersonDto : DataTransferObject
    {
        public long IdentificationNumber { get; set; }
        public string PersonName { get; set; }
        public string PersonLastName { get; set; }
        public DateTimeOffset PersonDateOfBirth { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public long PersonPhoneNumber { get; set; }
        public string PersonEmail { get; set; }
        public Guid DocumentTypeId { get; set; }
        public string DocumentType { get; set; } 
    }
}
