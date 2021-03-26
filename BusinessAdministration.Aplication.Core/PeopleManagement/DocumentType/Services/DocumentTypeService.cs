using AutoMapper;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.DocumentType;
using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.Services
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IDocumentTypeRepository _repoDocumentType;
        private readonly IMapper _mapper;

        public DocumentTypeService(IDocumentTypeRepository repo, IMapper mapper)
        {
            _repoDocumentType = repo;
            _mapper = mapper;
        }

        public async Task<Guid?> AddDocumentType(DocumentTypeDto request)
        {
            if (string.IsNullOrEmpty(request.DocumentType)) throw new DocumentTypeNotDefinedException();
            var response = await _repoDocumentType.Insert(_mapper.Map<DocumentTypeEntity>(request)).ConfigureAwait(false);
            return response.DocumentTypeId;
        }
        private static void ValidateRequireDocumentype(DocumentTypeDto request)
        {
            if (request.DocumentTypeId == Guid.Empty) throw new DocumentTypeIdNotDefinedException();
        }
        public bool DeleteDocumentType(DocumentTypeDto request)
        {
            ValidateRequireDocumentype(request);
            return _repoDocumentType.Delete(_mapper.Map<DocumentTypeEntity>(request));

        }

        public async Task<IEnumerable<DocumentTypeDto>> GetAll()
        {
            var response = _mapper.Map<IEnumerable<DocumentTypeDto>>(_repoDocumentType.GetAll<DocumentTypeEntity>());
            if (response.Count() == 0) throw new DocumentTypeEntityIsEmptyException();
            return response;
        }

        public bool UpdateDocumentType(DocumentTypeDto request)
        {
            ValidateRequireDocumentype(request);
            return _repoDocumentType.Update(_mapper.Map<DocumentTypeEntity>(request));
        }
    }
}
