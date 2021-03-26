using AutoMapper;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using BusinessAdministration.Domain.Core.PeopleManagement.Area;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Area.Mapping
{
    public class AreaProfile : Profile
    {
        public AreaProfile()
        {
            CreateMap<AreaEntity, AreaDto>().ReverseMap();
            CreateMap<AreaEntity, AreaRequestDto>().ReverseMap();
            CreateMap<AreaEntity, AreaResponseDto>().ReverseMap();
        }
    }
}
