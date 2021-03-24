using BusinessAdministration.Domain.Core.PeopleManagement;
using BusinessAdministration.Domain.Core.PeopleManagement.Area;
using BusinessAdministration.Domain.Core.PeopleManagement.Customer;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using BusinessAdministration.Domain.Core.PeopleManagement.Employed;
using BusinessAdministration.Domain.Core.PeopleManagement.Provider;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BusinessAdministration.Infrastructure.Data.Persistence.Core.Base
{
    internal class ContextDb : IContextDb
    {
        private readonly DbSettings _settings;

        #region Tables Db 
        public DbSet<PersonBase> Person => throw new System.NotImplementedException();

        public DbSet<DocumentTypeEntity> DocumentType => throw new System.NotImplementedException();

        public DbSet<AreaEntity> Area => throw new System.NotImplementedException();

        public DbSet<EmployedEntity> Employed => throw new System.NotImplementedException();

        public DbSet<ProviderEntity> Provider => throw new System.NotImplementedException();

        public DbSet<CustomerEntity> Customer => throw new System.NotImplementedException();

        #endregion

        public ContextDb(IOptions<DbSettings> settings) =>
           _settings = settings.Value;

        //TODO: Michael, implemented class
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(_settings.ConnectionString);

        public void Attach<T>(T item) where T : class
        {
            throw new System.NotImplementedException();
        }

        public int Commit() => base.SaveChanges();

        public void Rollback() =>
            base.ChangeTracker
            .Entries()
            .Where(e => e.Entity != null)
            .ToList()
            .ForEach(e => e.State = EntityState.Detached);

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public DbSet<T> Set<T>() where T : class
        {
            throw new System.NotImplementedException();
        }

        public void SetDeatached<T>(T item) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void SetModified<T>(T item) where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}
