using BusinessAdministration.Domain.Core.PeopleManagement.Area;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessAdministration.Domain.Core.PeopleManagement.Employed
{
    public class EmployedEntity : PersonBase
    {
        [Key]
        [StringLength(30)]
        public Guid EmployedId { get; set; }
        [Required]
        public Guid EmployeeCode { get; set; }
        public override PersonType PersonType => PersonType.naturalPerson;
        [Required]
        [StringLength(30)]
        public string EmployedPosition { get; set; }
        [Required]
        [StringLength(30)]
        public Guid AreaId { get; set; }
        public AreaEntity Area { get; set; }
        [Required]
        [StringLength(30)]
        public Guid DocumentTypeId { get; set; }
        public virtual DocumentTypeEntity DocumentType{ get; set; }
    }
}
