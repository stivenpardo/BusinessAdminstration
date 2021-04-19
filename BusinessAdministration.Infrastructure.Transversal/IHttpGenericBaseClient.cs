using BusinessAdministration.Aplication.Dto.Base;
using System.Threading.Tasks;

namespace BusinessAdministration.Infrastructure.Transversal
{
    public interface IHttpGenericBaseClient
    {
        Task<T> Get<T>(string path) where T : class;

        Task<TResponse> Post<TResponse, TRequest>(string path, TRequest request) where TRequest : DataTransferObject;

        Task<TResponse> Put<TResponse, TRequest>(string path, TRequest request) where TRequest : DataTransferObject;

        Task<TResponse> Patch<TResponse, TRequest>(string path, TRequest request) where TRequest : DataTransferObject;

        public Task<T> Delete<T>(string path) where T : DataTransferObject;
    }
}
