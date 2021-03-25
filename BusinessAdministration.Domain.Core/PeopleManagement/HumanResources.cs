using BusinessAdministration.Domain.Core.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessAdministration.Domain.Core.PeopleManagement
{
    public abstract class HumanResources : EntityBase
    {
        [Required]
        public DateTime CreationDate { get; set; }
    }
}
