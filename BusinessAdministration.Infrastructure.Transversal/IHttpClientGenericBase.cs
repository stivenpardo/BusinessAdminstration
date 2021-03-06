using BusinessAdministration.Aplication.Dto.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.Infrastructure.Transversal
{
    public interface IHttpClientGenericBase<T> where T : DataTransferObject
    {
        Task<IEnumerable<T>> Get(string action);

        Task<T> GetById(Guid id, string action);

        Task<T> Post(T request, string action);

        Task<T> Put(T request, string action);

        Task<T> Patch(T request, string action);

        public Task<T> Delete(Guid id, string action);
    }
}
