using AutoMapper;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Provider;
using BusinessAdministration.Domain.Core.PeopleManagement.Provider;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Provider.Mapping
{
    public class ProviderProfile : Profile
    {
        public ProviderProfile() =>
            CreateMap<ProviderEntity, ProviderDto>().ReverseMap();   
    }
}
