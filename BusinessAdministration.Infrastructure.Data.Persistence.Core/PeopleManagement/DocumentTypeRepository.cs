using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base;

namespace BusinessAdministration.Infrastructure.Data.Persistence.Core.PeopleManagement
{
    internal class DocumentTypeRepository : RepositoryBase<DocumentTypeEntity>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(IContextDb unitOfWork) : base(unitOfWork) { }
    }
}
