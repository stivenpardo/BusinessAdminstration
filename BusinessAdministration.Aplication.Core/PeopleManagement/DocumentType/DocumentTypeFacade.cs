using BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType;
using BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.Services;
using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType
{
    public class DocumentTypeFacade : IDocumentTypeFacade
    {
        private readonly IDocumentTypeService _documentTypeSVC;
        public DocumentTypeFacade(IDocumentTypeService documentTypeSVC) =>
            _documentTypeSVC = documentTypeSVC;

        public async Task<DocumentTypeResponseDto> CreateDocumentType(DocumentTypeDto request)
        {
            var response = await _documentTypeSVC.AddDocumentType(request).ConfigureAwait(false) != default;
            return new DocumentTypeResponseDto
            {
                StatusCode = response ? HttpStatusCode.OK : HttpStatusCode.Unauthorized,
                StatusDescription = response ? "Inserted documentType " : "Not inserted documentType",
            };
        }

        public DocumentTypeResponseDto DeleteDocumentType(DocumentTypeDto request)
        {
            var response = _documentTypeSVC.DeleteDocumentType(request);
            return new DocumentTypeResponseDto
            {
                StatusCode = response ? HttpStatusCode.OK : HttpStatusCode.Unauthorized,
                StatusDescription = response ? "Inserted documentType " : "Not documentType area",
            };
        }
        public Task<IEnumerable<DocumentTypeDto>> GetAllDocumentTypes() => _documentTypeSVC.GetAll();
        public DocumentTypeResponseDto UpdateDocumentType(DocumentTypeDto request)
        {
            var response = _documentTypeSVC.UpdateDocumentType(request);
            return new DocumentTypeResponseDto
            {
                StatusCode = response ? HttpStatusCode.OK : HttpStatusCode.Unauthorized,
                StatusDescription = response ? "Inserted documentType" : "Not inserted documentType",
            };
        }
        public async Task<string> DocumentTypeExportAll() =>
            throw new Exception();
        //await _documentTypeSVC.ExportAll().ConfigureAwait(false);
        public async Task<IEnumerable<DocumentTypeDto>> DocumentTypeImportAll() =>
            //await _documentTypeSVC.ImportAll().ConfigureAwait(false);
            throw new Exception();
    }
}
