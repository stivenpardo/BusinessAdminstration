using AutoMapper;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using BusinessAdministration.Domain.Core.PeopleManagement.Employed;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Employed.Mapping
{
    public class EmployedProfile : Profile
    {
        public EmployedProfile()
        {
            CreateMap<EmployedEntity, EmployedDto>().ReverseMap();
            CreateMap<EmployedEntity, EmployedRequestDto>().ReverseMap();
        }
    }
}
