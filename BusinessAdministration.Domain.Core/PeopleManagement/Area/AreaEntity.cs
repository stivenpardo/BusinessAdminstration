using BusinessAdministration.Domain.Core.PeopleManagement.Employed;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessAdministration.Domain.Core.PeopleManagement.Area
{
    public class AreaEntity
    {
        [Key]
        [StringLength(30)]
        public Guid AreaId { get; set; }
        [Required]
        [StringLength(30)]
        public string AreaName { get; set; }
        public EmployedEntity Person { get; set; }
    }
}
