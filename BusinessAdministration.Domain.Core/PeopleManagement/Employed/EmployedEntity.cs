using BusinessAdministration.Domain.Core.PeopleManagement.Area;
using BusinessAdministration.Domain.Core.PeopleManagement.Person;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessAdministration.Domain.Core.PeopleManagement.Employed
{
    public class EmployedEntity : HumanResources
    {
        [Key]
        [StringLength(30)]
        public Guid EmployedId { get; set; }
        [Required]
        [StringLength(30)]
        public string EmployedPosition { get; set; }
        [Required]
        [StringLength(30)]
        public Guid PersonId { get; set; }
        public PersonEntity Person { get; set; }
        [Required]
        [StringLength(30)]
        public Guid AreaId { get; set; }
        public AreaEntity Area { get; set; }

    }
}
