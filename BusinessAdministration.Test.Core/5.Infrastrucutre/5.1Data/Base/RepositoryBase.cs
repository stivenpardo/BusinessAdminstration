using BusinessAdministration.Domain.Core.PeopleManagement.Employed;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace BusinessAdministration.Test.Core._5.Infrastrucutre._5._1Data.Base
{
    public class RepositoryBase
    {
        //TODO:Michael, hacer el los test unitarios y de integración
        //para los metodos del repositoriobase
        //[Fact]
        //[UnitTest]
        //public async Task Test_insert_call_AddAsync_inunitofwork()
        //{       
        //    var unitOfWorkMock = new Mock<IContextDb>();
        //    unitOfWorkMock.Setup(m => m.Commit()).Returns(() =>
        //    {
        //        var response = 1;
        //        return response;
        //    });

        //    unitOfWorkMock.Setup(m => m.Set<EmployedEntity>()).Returns(() =>
        //    {
        //        var context = new ContextDb( new DbSettings 
        //        { 
        //            ConnectionString = "Server = DESKTOP-A52QQCF\\SQLEXPRESS; Database = BusinessAdministration; Trusted_Connection = True;"
        //        });
        //        context.Employed.Add(new EmployedEntity
        //        {
        //            EmployedId = Guid.NewGuid(),
                 
        //        });
        //        context.SaveChanges();
        //        return context.Employed;
        //    });

        //    var services = new ServiceCollection();
        //    services.AddTransient(_ => unitOfWorkMock.Object);
        //    services.ConfigureBaseRepository(new DbSettings());
        //    var provider = services.BuildServiceProvider();
        //    var sumaSvc = provider.GetRequiredService<IEmployedRepository>();

        //    var response = sumaSvc.Insert(new EmployedEntity { EmployedId = Guid.NewGuid()});
        //    unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        //    unitOfWorkMock.Verify(x => x.Set<EmployedEntity>(), Times.Once);
        //}
    }
}
