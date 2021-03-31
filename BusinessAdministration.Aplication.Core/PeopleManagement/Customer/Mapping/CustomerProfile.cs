using AutoMapper;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using BusinessAdministration.Domain.Core.PeopleManagement.Customer;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Customer.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() =>
            CreateMap<CustomerEntity, CustomerDto>().ReverseMap();   
    }
}
