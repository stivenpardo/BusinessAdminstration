using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.HttpClientDocumentType
{
    public interface IDocumentTypeHttpClient
    {
        Task<IEnumerable<DocumentTypeDto>> GetAll();
        Task<DocumentTypeDto> GetById(Guid id);
        Task<DocumentTypeDto> Create(DocumentTypeDto request);
        Task<DocumentTypeDto> Update(DocumentTypeDto request);
        Task<DocumentTypeDto> Delete(Guid id);
    }
}
