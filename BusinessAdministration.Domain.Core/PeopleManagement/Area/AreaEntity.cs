using BusinessAdministration.Domain.Core.Base;
using BusinessAdministration.Domain.Core.PeopleManagement.Employed;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessAdministration.Domain.Core.PeopleManagement.Area
{
    public class AreaEntity : EntityBase
    {
        [Key]
        [StringLength(30)]
        public Guid AreaId { get; set; }
        [Required]
        [StringLength(30)]
        public string AreaName { get; set; }
        [Required]
        [StringLength(30)]
        public Guid LiableEmployerId { get; set; }
        public virtual IEnumerable<EmployedEntity> EmployeesList { get; set; }
    }
}
