using BusinessAdministration.Domain.Core.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinessAdministration.Infrastructure.Data.Persistence.Core.Base
{
    public abstract class RepositoryBase<TGeneric> : IRepositoryBase<TGeneric> where TGeneric : EntityBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public IUnitOfWork UnitOfWork => _unitOfWork;

        public RepositoryBase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<T> Insert<T>(T entity) where T : EntityBase
        {
            try
            {
                var response = await _unitOfWork.Set<T>().AddAsync(entity);
                _unitOfWork.Commit();
                return response.Entity;
            }
            catch (Exception)
            {
                throw new Exception($"No se pudo realizar el registro de la entidad {entity}");
            }

        }
        public bool Update<T>(T entity) where T : EntityBase
        {
            try
            {
                var response = _unitOfWork.Set<T>().Update(entity);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete<T>(T entity) where T : EntityBase
        {
            try
            {
                var entityToDelete = _unitOfWork.Set<T>().First(x => x == entity);
                _unitOfWork.Set<T>().Remove(entityToDelete);
                _unitOfWork.Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<T> SearchMatching<T>(Expression<Func<T, bool>> predicate) where T : EntityBase =>
            _unitOfWork.Set<T>().Where(predicate);

        public IEnumerable<T> GetAll<T>() where T : EntityBase => _unitOfWork.Set<T>().ToArray();

        public T SearchMatchingOneResult<T>(Expression<Func<T, bool>> predicate) where T : EntityBase =>
            _unitOfWork.Set<T>().First(predicate);

    }
}
