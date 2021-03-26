using AutoMapper;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.DocumentType;
using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DocumentTypeDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool UpdateDocumentType(DocumentTypeDto request)
        {
            ValidateRequireDocumentype(request);

            throw new NotImplementedException();
        }
    }
}
