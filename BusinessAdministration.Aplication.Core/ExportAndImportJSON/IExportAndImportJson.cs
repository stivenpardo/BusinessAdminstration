using BusinessAdministration.Aplication.Dto.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.ExportAndImportJSON
{
    public interface IExportAndImportJson
    {
        Task<string> ExportJson<TRequest>(string path, TRequest request) where TRequest : IEnumerable<DataTransferObject>;
        Task<TResponse> ImportJson<TResponse>(string path) where TResponse : IEnumerable<DataTransferObject>;
    }
}
