using Microsoft.EntityFrameworkCore;
using System;

namespace BusinessAdministration.Domain.Core.Base
{
    public interface IUnitOfWork : IDisposable
    {
        public int Commit();
        public void RollBack();
        public DbSet<T> Set<T>() where T : EntityBase;
        public void Attach<T>(T item) where T : EntityBase;
        public void SetModified<T>(T item) where T : EntityBase;
        public void SetDeatached<T>(T item) where T : EntityBase;
    }
}
