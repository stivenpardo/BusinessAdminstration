using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessAdministration.Domain.Core.PeopleManagement.DocumentType
{
    public class DocumentTypeEntity
    {
        [Key]
        [StringLength(30)]
        public Guid DocumentTypeId { get; set; }
        [Required]
        [StringLength(30)]
        public string DocumentType { get; set; }
        [Required]
        public int DocumentTypeNumber { get; set; }
        public PersonBase Person { get; set; }
    }
}
