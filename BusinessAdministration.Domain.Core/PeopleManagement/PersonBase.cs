using BusinessAdministration.Domain.Core.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessAdministration.Domain.Core.PeopleManagement
{
    public enum PersonType
    {
        naturalPerson = 1,
        CorporatePerson = 2,
    }
    public abstract class PersonBase : EntityBase
    {
        public long IdentificationNumber { get; set; }
        [Required]
        public virtual PersonType PersonType { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
        [Required]
        public DateTimeOffset DateOfBirth { get; set; }
        [Required]
        public DateTimeOffset creationDate { get; set; }
        [Required]
        public long PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(30)]
        public string PersonEmail { get; set; }
    }
}
