using BusinessAdministration.Domain.Core.Base;
using BusinessAdministration.Domain.Core.PeopleManagement.Customer;
using BusinessAdministration.Domain.Core.PeopleManagement.Employed;
using BusinessAdministration.Domain.Core.PeopleManagement.Provider;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessAdministration.Domain.Core.PeopleManagement.DocumentType
{
    public class DocumentTypeEntity : EntityBase
    {
        [Key]
        [StringLength(30)]
        public Guid DocumentTypeId { get; set; }
        [Required]
        [StringLength(30)]
        public string DocumentType { get; set; }
        public virtual IEnumerable<ProviderEntity> ProvidersList { get; set; }
        public virtual IEnumerable<CustomerEntity> CustomerList { get; set; }
        public virtual IEnumerable<EmployedEntity> EmployeesList { get; set; }
    }
}
