using BusinessAdministration.Domain.Core.PeopleManagement;
using System;

namespace BusinessAdministration.Aplication.Dto.PeopleManagement.Provider
{
    public class ProviderDto : PersonDto
    {
        public Guid ProviderId { get; set; }
        public string PersonBusinessName { get; set; }
        public PersonType PersonType { get; set; }
    }
}
