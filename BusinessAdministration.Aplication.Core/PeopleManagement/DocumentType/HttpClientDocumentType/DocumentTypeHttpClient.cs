using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using BusinessAdministration.Infrastructure.Transversal;
using BusinessAdministration.Infrastructure.Transversal.Configurator;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.HttpClientDocumentType
{
    public class DocumentTypeHttpClient : HttpClientGenericBase<DocumentTypeDto>, IDocumentTypeHttpClient
    {
        public DocumentTypeHttpClient(HttpClient client, IOptions<HttpClientSettings> settings) : base(client, settings)
        {
        }

        protected override string Controller { get => "/DocumentType"; }

        public async Task<IEnumerable<DocumentTypeDto>> GetAll() =>
            await Get("getalldocumenttypes").ConfigureAwait(false);

        public async Task<DocumentTypeDto> Create(DocumentTypeDto request) =>
            await Post(request, "CreateDocumentType").ConfigureAwait(false);


        public async Task<DocumentTypeDto> Update(DocumentTypeDto request) =>
            await Put(request, "UpdateDocumentType").ConfigureAwait(false);

        public async Task<DocumentTypeDto> Delete(DocumentTypeDto request) =>
            await Delete(request, "DeleteDocumentType").ConfigureAwait(false);
    }
}
