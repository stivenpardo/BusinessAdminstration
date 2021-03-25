using BusinessAdministration.Domain.Core.Base;
using BusinessAdministration.Domain.Core.PeopleManagement.Area;
using BusinessAdministration.Domain.Core.PeopleManagement.Customer;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using BusinessAdministration.Domain.Core.PeopleManagement.Employed;
using BusinessAdministration.Domain.Core.PeopleManagement.Provider;
using Microsoft.EntityFrameworkCore;
using System;

namespace BusinessAdministration.Infrastructure.Data.Persistence.Core.Base
{
    public interface IContextDb : IUnitOfWork, IDisposable
    {
        DbSet<DocumentTypeEntity> DocumentType { get; }
        DbSet<AreaEntity> Area { get; }
        DbSet<EmployedEntity> Employed { get; }
        DbSet<ProviderEntity> Provider { get; }
        DbSet<CustomerEntity> Customer { get; }
    }
}
