using BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType;
using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        private readonly ILogger<DocumentTypeController> _logger;
        private readonly IDocumentTypeFacade _documentTypeFacade;

        public DocumentTypeController(ILogger<DocumentTypeController> logger, IDocumentTypeFacade documentTypeFacade)
        {
            _logger = logger;
            _documentTypeFacade = documentTypeFacade;
        }

        [HttpPost(nameof(CreateDocumentType))]
        public async Task<DocumentTypeResponseDto> CreateDocumentType(DocumentTypeDto request) =>
            await _documentTypeFacade.CreateDocumentType(request).ConfigureAwait(false);

        [HttpGet(nameof(GetAllDocumentTypes))]
        public async Task<IEnumerable<DocumentTypeDto>> GetAllDocumentTypes() =>
             await _documentTypeFacade.GetAllDocumentTypes().ConfigureAwait(false);

        [HttpPut(nameof(UpdateDocumentType))]
        public DocumentTypeResponseDto UpdateDocumentType(DocumentTypeDto request) =>
            _documentTypeFacade.UpdateDocumentType(request);

        [HttpDelete(nameof(DeleteDocumentType))]
        public DocumentTypeResponseDto DeleteDocumentType(DocumentTypeDto request) =>
            _documentTypeFacade.DeleteDocumentType(request);
    }
}
