using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.Employed.Services;
using BusinessAdministration.Domain.Core.PeopleManagement.Employed;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace BusinessAdministration.Test.Core._3.Application.Core.PeopleManagement.Customer
{
    public class GetAllEmployedTest
    {

        [Fact]
        [UnitTest]
        public async Task Get_All_Successful()
        {
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
                .Setup(m => m.GetAll<EmployedEntity>())
                .Returns(new List<EmployedEntity> { new EmployedEntity
                {
                    EmployedId= Guid.NewGuid(),
                    EmployedCode= Guid.NewGuid(),
                },
                 new EmployedEntity
                {
                    EmployedId= Guid.NewGuid(),
                    EmployedCode= Guid.NewGuid()
                }});
            var service = new ServiceCollection();
            service.AddTransient(_ => employedRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            var response = await employedSvc.GetAll().ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.NotEqual(default, response);
        }
        [Fact]
        [IntegrationTest]
        public async Task GetAllEmployed_Successfull_IntegrationTest()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings
            {
                ConnectionString = "Data Source=DESKTOP-A52QQCF\\SQLEXPRESS;Initial Catalog=BusinessAdministration;Integrated Security=True"
            });
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();
            var responseSearch = await employedSvc.GetAll().ConfigureAwait(false);
            Assert.NotEqual(default, responseSearch);
        }

    }
}
