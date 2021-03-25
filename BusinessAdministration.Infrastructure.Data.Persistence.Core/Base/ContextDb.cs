using BusinessAdministration.Domain.Core.Base;
using BusinessAdministration.Domain.Core.PeopleManagement.Area;
using BusinessAdministration.Domain.Core.PeopleManagement.Customer;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using BusinessAdministration.Domain.Core.PeopleManagement.Employed;
using BusinessAdministration.Domain.Core.PeopleManagement.Provider;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;

namespace BusinessAdministration.Infrastructure.Data.Persistence.Core.Base
{
    internal class ContextDb : DbContext, IContextDb
    {
        private readonly DbSettings _settings;

        #region Tables Db 
        public DbSet<DocumentTypeEntity> DocumentType { get; set; }

        public DbSet<AreaEntity> Area { get; set; }

        public DbSet<EmployedEntity> Employed { get; set; }

        public DbSet<ProviderEntity> Provider { get; set; }

        public DbSet<CustomerEntity> Customer { get; set; }
        #endregion Tables Db

        public ContextDb(IOptions<DbSettings> settings) =>
            _settings = settings.Value;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
           optionsBuilder.UseSqlServer(_settings.ConnectionString);
        public int Commit() => base.SaveChanges();
        public void RollBack() =>
            base.ChangeTracker
            .Entries()
            .Where(e => e.Entity != null)
            .ToList()
            .ForEach(e => e.State = EntityState.Detached);

        public new DbSet<T> Set<T>() where T : EntityBase => base.Set<T>();

        public void SetDeatached<T>(T item) where T : EntityBase => Entry(item).State = EntityState.Detached;
        public void SetModified<T>(T item) where T : EntityBase => Entry(item).State = EntityState.Modified;
        public void Attach<T>(T item) where T : EntityBase
        {
            if (Entry(item).State == EntityState.Detached)
                base.Set<T>().Attach(item);
        }
    }
}
