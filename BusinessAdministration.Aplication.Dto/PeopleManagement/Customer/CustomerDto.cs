using BusinessAdministration.Domain.Core.PeopleManagement;
using System;

namespace BusinessAdministration.Aplication.Dto.PeopleManagement.Employed
{
    public class CustomerDto : PersonDto
    {
        public Guid CustomerId { get; set; }
        public PersonType PersonType { get; set; }
    }
}
