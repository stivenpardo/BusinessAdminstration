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
        public Guid PersonId { get; set; }
        public PersonBase Person { get; set; }
    }
}
