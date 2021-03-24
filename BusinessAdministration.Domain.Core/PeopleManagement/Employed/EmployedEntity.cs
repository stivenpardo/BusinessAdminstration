using BusinessAdministration.Domain.Core.PeopleManagement.Area;
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
        [StringLength(30)]
        public Guid PersonId { get; set; }
        public PersonBase Person { get; set; }
        [Required]
        [StringLength(30)]
        public Guid AreaId { get; set; }
        public AreaEntity Area { get; set; }

    }
}
