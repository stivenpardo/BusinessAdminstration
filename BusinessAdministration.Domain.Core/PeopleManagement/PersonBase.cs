using BusinessAdministration.Domain.Core.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessAdministration.Domain.Core.PeopleManagement
{
    public enum PersonType
    {
        NaturalPerson = 1,
        CorporatePerson = 2,
    }
    public abstract class PersonBase : EntityBase
    {
        [Required]
        public long IdentificationNumber { get; set; }
        [Required]
        public virtual PersonType PersonType { get; set; }
        [Required]
        [StringLength(30)]
        public string PersonName { get; set; }
        [Required]
        [StringLength(30)]
        public string PersonLastName { get; set; }
        [Required]
        public DateTimeOffset PersonDateOfBirth { get; set; }
        [Required]
        public DateTimeOffset CreationDate { get; set; }
        [Required]
        public long PersonPhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(30)]
        public string PersonEmail { get; set; }
    }
}
