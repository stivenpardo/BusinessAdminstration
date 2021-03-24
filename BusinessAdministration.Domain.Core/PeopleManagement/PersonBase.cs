using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessAdministration.Domain.Core.PeopleManagement
{
    public enum PersonType
    {
        naturalPerson = 1,
        CorporatePerson =2,
    }
    public abstract class PersonBase
    {
        [Key]
        [StringLength(30)]
        public Guid PersonId { get; set; }
        [Required]
        public PersonType PersonType { get; set; }
        [Required]
        [StringLength(30)]
        public string PersonName { get; set; }
        [Required]
        [StringLength(30)]
        public string PersonLastName { get; set; }
        [Required]
        public DateTime PersonDateOfBirth { get; set; }
        [Required]
        public DateTime PersonCreationDate { get; set; }
        [Required]
        public int PersonPhoneNumber{ get; set; }
        [Required]
        [EmailAddress]
        [StringLength(30)]
        public string PersonEmail{ get; set; }
        [Required]
        [StringLength(30)]
        public Guid DocumentTypeId { get; set; }
        public DocumentTypeEntity DocumentType { get; set; }

    }
}
