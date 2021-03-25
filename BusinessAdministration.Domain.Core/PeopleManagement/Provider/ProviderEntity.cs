using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessAdministration.Domain.Core.PeopleManagement.Provider
{
    public class ProviderEntity : PersonBase
    {
        [Key]
        [StringLength(30)]
        public Guid ProviderId { get; set; }
        [Required]
        [StringLength(50)]
        public string PersonBusinessName { get; set; }
        [Required]
        [StringLength(30)]
        public Guid DocumentTypeId { get; set; }
        public DocumentTypeEntity DocumentType { get; set; }
    }
}
