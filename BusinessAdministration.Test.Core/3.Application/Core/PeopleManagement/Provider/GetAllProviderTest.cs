using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.Provider.Services;
using BusinessAdministration.Domain.Core.PeopleManagement.Provider;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace BusinessAdministration.Test.Core._3.Application.Core.PeopleManagement.Provider
{
    public class GetAllProviderTest
    {
        [Fact]
        [UnitTest]
        public async Task Get_All_Successful()
        {
            var providerRepoMock = new Mock<IProviderRepository>();
            providerRepoMock
                .Setup(m => m.GetAll<ProviderEntity>())
                .Returns(new List<ProviderEntity> { new ProviderEntity
                {
                    ProviderId= Guid.NewGuid(),
                    DocumentTypeId= Guid.NewGuid(),
                },
                 new ProviderEntity
                {
                    ProviderId= Guid.NewGuid(),
                    DocumentTypeId= Guid.NewGuid(),
                }});
            var service = new ServiceCollection();
            service.AddTransient(_ => providerRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var providerSvc = provider.GetRequiredService<IProviderService>();
            var response = await providerSvc.GetAll().ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.NotEqual(default, response);
        }
        [Fact]
        [IntegrationTest]
        public async Task GetAllPoviders_Successfull_IntegrationTest()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings
            {
                ConnectionString = "Data Source=DESKTOP-A52QQCF\\SQLEXPRESS;Initial Catalog=BusinessAdministration;Integrated Security=True"
            });
            var provider = service.BuildServiceProvider();
            var providerSvc = provider.GetRequiredService<IProviderService>();
            var responseSearch = await providerSvc.GetAll().ConfigureAwait(false);
            Assert.NotEqual(default, responseSearch);
        }
    }
}
