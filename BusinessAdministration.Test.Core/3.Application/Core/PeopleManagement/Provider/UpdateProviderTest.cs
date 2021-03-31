using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.Customer.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person;
using BusinessAdministration.Aplication.Core.PeopleManagement.Provider.Services;
using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Provider;
using BusinessAdministration.Domain.Core.PeopleManagement;
using BusinessAdministration.Domain.Core.PeopleManagement.Customer;
using BusinessAdministration.Domain.Core.PeopleManagement.Provider;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace BusinessAdministration.Test.Core._3.Application.Core.PeopleManagement.Provider
{
    public class UpdateProviderTest
    {
        [Fact]
        [UnitTest]
        public void UpdateProviders_Throw_Exception_when_ProviderId_is_null_or_empty()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var providerSvc = provider.GetRequiredService<IProviderService>();

            Assert.Throws<IdCannotNullOrEmptyException>(() => providerSvc.UpdateProvider(new ProviderDto
            {
                ProviderId = Guid.Empty,
                DocumentTypeId = Guid.NewGuid(),
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
                DocumentTypeId = Guid.NewGuid()
            };
            Assert.Throws<DontExistIdException>(() => providerSvc.UpdateProvider(newProvider));
        }

        [Fact]
        [UnitTest]
        public void UpdateProvider_Successfult_Test()
        {
            var providerRepoMock = new Mock<IProviderRepository>();
            providerRepoMock
               .Setup(x => x.SearchMatching(It.IsAny<Expression<Func<ProviderEntity, bool>>>()))
               .Returns(new List<ProviderEntity> { new ProviderEntity
               {
                   ProviderId = Guid.NewGuid()
               }});
            providerRepoMock
                 .Setup(x => x.Update(It.IsAny<ProviderEntity>()))
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
                ProviderId = Guid.NewGuid(),
                PersonName = "NAME FAKE"
            };
            var response = providerSvc.UpdateProvider(newProvider);
            Assert.NotEqual(default, response);
            Assert.True(response);
        }
        [Fact]
        [IntegrationTest]
        public async Task UpdateProvider_Successfull_IntegrationTest()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings
            {
                ConnectionString = "Data Source=DESKTOP-A52QQCF\\SQLEXPRESS;Initial Catalog=BusinessAdministration;Integrated Security=True"
            });
            var provider = service.BuildServiceProvider();
            var providerSvc = provider.GetRequiredService<IProviderService>();
            var documentTypeSvc = provider.GetRequiredService<IDocumentTypeService>();

            var newDocumentType = new DocumentTypeDto
            {
                DocumentTypeId = Guid.NewGuid(),
                DocumentType = "Nit"
            };
            var responseAddDocumentType = await documentTypeSvc.AddDocumentType(newDocumentType).ConfigureAwait(false);
            var newProvider = new ProviderDto
            {
                PersonType = PersonType.NaturalPerson,
                DocumentTypeId = Guid.Parse(responseAddDocumentType.ToString()),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                PersonLastName = "lastName fake",
                PersonBusinessName = "fAKEnAME",
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                PersonPhoneNumber = 3212224534,
                PersonEmail = "Fake@gmail.com"
            };
            var responseAddProvider = await providerSvc.AddProvider(newProvider).ConfigureAwait(false);
            var UpdateProvider = new ProviderDto
            {
                ProviderId = Guid.Parse(responseAddProvider.ToString()),
                PersonType = PersonType.NaturalPerson,
                DocumentTypeId = Guid.Parse(responseAddDocumentType.ToString()),
                IdentificationNumber = 133,
                PersonName = "updateName fake",
                PersonLastName = "update fake",
                PersonBusinessName = "fAKEnAME123",
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                PersonPhoneNumber = 3212224534,
                PersonEmail = "Fake-update@gmail.com"
            };
            var responseUpdateProvider = providerSvc.UpdateProvider(UpdateProvider);
            var providerDelete = new ProviderDto
            {
                ProviderId = Guid.Parse(responseAddProvider.ToString())
            };
            var responseDeleteProvider = providerSvc.DeleteProvider(providerDelete);
            var responseDeleteDocumentType = documentTypeSvc.DeleteDocumentType(newDocumentType);

            Assert.NotNull(responseAddDocumentType);
            Assert.NotEqual(default, responseAddDocumentType);
            Assert.NotEqual(default, responseAddProvider);
            Assert.True(responseUpdateProvider);
            Assert.True(responseDeleteDocumentType);
            Assert.True(responseDeleteProvider);
        }
    }

}

