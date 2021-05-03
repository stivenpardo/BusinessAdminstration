using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.HttpClientDocumentType
{
    public interface IDocumentTypeHttpClient
    {
        Task<IEnumerable<DocumentTypeDto>> GetAll();
        Task<DocumentTypeDto> Create(DocumentTypeDto request);
    }
}
