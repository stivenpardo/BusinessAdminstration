using AutoMapper;
using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.Mapping
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<DocumentTypeEntity, DocumentTypeDto>().ReverseMap();
        }
    }
}
