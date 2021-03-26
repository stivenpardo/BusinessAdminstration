using BusinessAdministration.Aplication.Dto.Base;
using System;

namespace BusinessAdministration.Aplication.Dto.PeopleManagement
{
    public class PersonDto : DataTransferObject
    {
        public long IdentificationNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public DateTimeOffset creationDate { get; set; }
        public long PhoneNumber { get; set; }
        public string PersonEmail { get; set; }
        public Guid DocumentTypeId { get; set; }
    }
}
