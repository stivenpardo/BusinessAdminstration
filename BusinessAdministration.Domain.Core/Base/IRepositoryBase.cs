using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinessAdministration.Domain.Core.Base
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        IUnitOfWork UnitOfWork { get; }
        public Task<T> Insert<T>(T entity) where T : EntityBase;
        public bool Update<T>(T entity) where T : EntityBase;
        public bool Delete<T>(T entity) where T : EntityBase;
        public IEnumerable<T> SearchMatching<T>(Expression<Func<T, bool>> predicate) where T : EntityBase;
        public T SearchMatchingOneResult<T>(Expression<Func<T, bool>> predicate) where T : EntityBase;
        public IEnumerable<T> GetAll<T>() where T : EntityBase;
    }
}
