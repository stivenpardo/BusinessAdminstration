using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessAdministration.Domain.Core.PeopleManagement.Customer
{
    public class CustomerEntity : PersonBase
    {
        [Key]
        [StringLength(30)]
        public Guid CustomerId { get; set; }
        [Required]
        [StringLength(30)]
        public Guid DocumentTypeId { get; set; }
        public DocumentTypeEntity DocumentType { get; set; }
    }
}
