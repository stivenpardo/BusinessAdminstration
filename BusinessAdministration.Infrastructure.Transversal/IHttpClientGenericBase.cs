using BusinessAdministration.Aplication.Dto.Base;
using System.Threading.Tasks;

namespace BusinessAdministration.Infrastructure.Transversal
{
    public interface IHttpClientGenericBase<T> where T : DataTransferObject
    {
        Task<T> Get();

        Task<T> Post(T request);

        Task<T> Put(T request);

        Task<T> Patch(T request);

        public Task<T> Delete();
    }
}
