using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType
{
    public interface IDocumentTypeFacade
    {
        public Task<DocumentTypeResponseDto> CreateDocumentType(DocumentTypeDto request);
        public Task<IEnumerable<DocumentTypeDto>> GetAllDocumentTypes();
        public DocumentTypeResponseDto UpdateDocumentType(DocumentTypeDto request);
        public DocumentTypeResponseDto DeleteDocumentType(DocumentTypeDto request);
        public Task<string> DocumentTypeExportAll();
        public Task<IEnumerable<DocumentTypeDto>> DocumentTypeImportAll();
    }
}
