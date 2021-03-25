using BusinessAdministration.Domain.Core.PeopleManagement.Person;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessAdministration.Domain.Core.PeopleManagement.Customer
{
    public class CustomerEntity : HumanResources
    {
        [Key]
        [StringLength(30)]
        public Guid CustomerId { get; set; }
        [Required]
        [StringLength(30)]
        public Guid PersonId { get; set; }
        public PersonEntity Person { get; set; }
    }
}
