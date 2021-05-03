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
        public DocumentTypeHttpClient(HttpClient client, IOptions<HttpClientSettings> settings) : base(client, settings) { }

        protected override string Controller { get => "/DocumentType"; }

        public async Task<IEnumerable<DocumentTypeDto>> GetAll() =>
            await Get("/getalldocumenttypes").ConfigureAwait(false);
    }
}
