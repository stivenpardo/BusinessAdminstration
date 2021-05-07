using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.Services
{
    public interface IDocumentTypeService
    {
        public Task<Guid?> AddDocumentType(DocumentTypeDto request);
        public Task<IEnumerable<DocumentTypeDto>> GetAll();
        public Task<DocumentTypeDto> GetById(Guid id);
        public bool UpdateDocumentType(DocumentTypeDto request);
        public bool DeleteDocumentType(DocumentTypeDto request);
    }

}
