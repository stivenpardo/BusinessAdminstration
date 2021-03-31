using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person;
using BusinessAdministration.Aplication.Core.PeopleManagement.Provider.Services;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Provider;
using BusinessAdministration.Domain.Core.PeopleManagement.Provider;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;
using Xunit.Categories;

namespace BusinessAdministration.Test.Core._3.Application.Core.PeopleManagement.Provider
{
    public class DeleteProviderTest
    {
        [Fact]
        [UnitTest]
        public void DeleteProvider_Throw_IdCannotNullOrEmptyException_when_ProviderId_is_null_or_empty()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var providerSvc = provider.GetRequiredService<IProviderService>();

            Assert.Throws<IdCannotNullOrEmptyException>(() => providerSvc.DeleteProvider(new ProviderDto
            {
                ProviderId = Guid.Empty
            }));
        }
        [Fact]
        [UnitTest]
        public void Throw_DontExistIdException_when_id_it_isnt()
        {
            var providerRepoMock = new Mock<IProviderRepository>();
            providerRepoMock
                 .Setup(x => x.SearchMatching(It.IsAny<Expression<Func<ProviderEntity, bool>>>()))
                 .Returns(new List<ProviderEntity>());
            var service = new ServiceCollection();
            service.AddTransient(_ => providerRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var providerSvc = provider.GetRequiredService<IProviderService>();

            var newProvider = new ProviderDto
            {
                ProviderId = Guid.NewGuid(),
            };
            Assert.Throws<DontExistIdException>(() => providerSvc.DeleteProvider(newProvider));
        }
        [Fact]
        [UnitTest]
        public void DeleteProvider_Successfult_Test()
        {
            var providerRepoMock = new Mock<IProviderRepository>();
            providerRepoMock
                .Setup(e => e.SearchMatching(It.IsAny<Expression<Func<ProviderEntity, bool>>>()))
                .Returns(new List<ProviderEntity> { new ProviderEntity
                {
                    ProviderId = Guid.NewGuid()
                }});

            providerRepoMock
                .Setup(e => e.Delete(It.IsAny<ProviderEntity>()))
                .Returns(() =>
               {
                   return true;
               });

            var service = new ServiceCollection();
            service.AddTransient(_ => providerRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var providerSvc = provider.GetRequiredService<IProviderService>();

            var newProvider = new ProviderDto
            {
                ProviderId = Guid.Parse("31826538-6b06-4021-95c2-27fb184ac4fe")
            };

            var responseDelete = providerSvc.DeleteProvider(newProvider);
            Assert.NotEqual(default, responseDelete);
            Assert.True(responseDelete);
        }
    }
}
