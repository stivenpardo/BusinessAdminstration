using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.DocumentType;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person;
using BusinessAdministration.Aplication.Core.PeopleManagement.Provider.Services;
using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Provider;
using BusinessAdministration.Domain.Core.PeopleManagement;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
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
    public class AddProviderTest
    {
        [Fact]
        [UnitTest]
        public async Task AddProvider_Throw_Exception_when_properties_requires_are_null_or_empty()
        {
            var provider = GetProviderWithoutMock();
            var providerSvc = provider.GetRequiredService<IProviderService>();

            await Assert.ThrowsAsync<DocumentTypeIdNotDefinedException>(() => providerSvc.AddProvider(new ProviderDto
            {
                DocumentTypeId = Guid.Empty,
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
            })).ConfigureAwait(false);

            await Assert.ThrowsAsync<DateOfBirthNotDefinedException>(() => providerSvc.AddProvider(new ProviderDto
            {
                DocumentTypeId = Guid.NewGuid(),
                PersonDateOfBirth = default,
                CreationDate = DateTimeOffset.Now,
            })).ConfigureAwait(false);
        }

        [Fact]
        [UnitTest]
        public async Task Throw_CreationDateNotDefinedException_when_DateOfBirth_is_null_or_default()
        {
            var provider = GetProviderWithoutMock();
            var providerSvc = provider.GetRequiredService<IProviderService>();

            await Assert.ThrowsAsync<CreationDateNotDefinedException>(() => providerSvc.AddProvider(new ProviderDto
            {
                DocumentTypeId = Guid.NewGuid(),
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = default,
            })).ConfigureAwait(false);
        }

        [Fact]
        [UnitTest]
        public async Task Throw_AlreadyExistException_when_there_are_two_persons_with_same_Name_and_Business_name()
        {
            var providerRepoMock = new Mock<IProviderRepository>();
            providerRepoMock
                .Setup(m => m.GetAll<ProviderEntity>())
                .Returns(new List<ProviderEntity> { new ProviderEntity
                {
                   ProviderId = Guid.NewGuid(),
                   PersonName = "Pepito",
                   PersonBusinessName = "NameFake"

                }});

            var service = new ServiceCollection();
            service.AddTransient(_ => providerRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var providerSvc = provider.GetRequiredService<IProviderService>();

            var newProvider = new ProviderDto
            {
                DocumentTypeId = Guid.NewGuid(),
                IdentificationNumber = 123,
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                PersonName = "Pepito",
                PersonBusinessName = "NameFake"
            };
            var response = await Assert.ThrowsAsync<AlreadyExistException>(() =>
                providerSvc.AddProvider(newProvider)).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.Equal($"ya existe alguien con el nombre : {newProvider.PersonName} y razon social:{newProvider.PersonBusinessName}", response.Message);
        }
        [Fact]
        [UnitTest]
        public async Task Throw_AlreadyExistException_when_there_are_two_persons_with_same_IdentificationNumber_and_typeDocument()
        {
            var providerRepoMock = new Mock<IProviderRepository>();
            providerRepoMock
                .Setup(m => m.GetAll<ProviderEntity>())
                .Returns(new List<ProviderEntity> { new ProviderEntity
                {
                   DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0592"),
                   IdentificationNumber = 123,
                   PersonName="Juanito",
                   PersonBusinessName = "NameFake"
                }});

            var service = new ServiceCollection();
            service.AddTransient(_ => providerRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var providerSvc = provider.GetRequiredService<IProviderService>();

            var newProvider = new ProviderDto
            {
                DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0592"),
                IdentificationNumber = 123,
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                PersonBusinessName = "OtherNameFake"

            };
            var response = await Assert.ThrowsAsync<AlreadyExistException>(() =>
                providerSvc.AddProvider(newProvider)).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.Equal($"ya existe alguien el mismo numero de indentificación y tipo de documento: { newProvider.IdentificationNumber}", response.Message);
        }

        [Fact]
        [UnitTest]
        public async Task Throws_NoExistDocumentTypeException_when_DoncumentType_dont_exist()
        {
            var providerRepoMock = new Mock<IProviderRepository>();
            providerRepoMock
                .Setup(m => m.GetAll<ProviderEntity>())
                .Returns(new List<ProviderEntity> { new ProviderEntity
                {
                   DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0592"),
                   IdentificationNumber = 183,
                   PersonName="Juanito",
                   DocumentType = new DocumentTypeEntity { DocumentType = "Nit"},
                   PersonBusinessName = "NameFake"
                }});
            var DocumentTypeRepoMock = new Mock<IDocumentTypeRepository>();
            DocumentTypeRepoMock
                .Setup(m => m.SearchMatching(It.IsAny<Expression<Func<DocumentTypeEntity, bool>>>()))
                .Returns(new List<DocumentTypeEntity>());

            var service = new ServiceCollection();
            service.AddTransient(_ => providerRepoMock.Object);
            service.AddTransient(_ => DocumentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var providerSvc = provider.GetRequiredService<IProviderService>();

            var newProvider = new ProviderDto
            {
                DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0597"),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                PersonBusinessName = "OtherNameFake"
            };
            var response = await Assert.ThrowsAsync<NoExistDocumentTypeException>(() =>
                providerSvc.AddProvider(newProvider)).ConfigureAwait(false);
        }

        [Fact]
        [UnitTest]
        public async Task Throws_CannotBeNaturalPersonException_when_Person_is_have_document_type_diferent_NIT()
        {
            var providerRepoMock = new Mock<IProviderRepository>();
            providerRepoMock
                .Setup(m => m.GetAll<ProviderEntity>())
                .Returns(new List<ProviderEntity> { new ProviderEntity
                {
                   DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0592"),
                   IdentificationNumber = 183,
                   PersonName="Juanito",
                   DocumentType = new DocumentTypeEntity { DocumentType = "Nit"},
                   PersonBusinessName = "NameFake"
                }});
            var DocumentTypeRepoMock = new Mock<IDocumentTypeRepository>();
            DocumentTypeRepoMock
                .Setup(m => m.SearchMatching(It.IsAny<Expression<Func<DocumentTypeEntity, bool>>>()))
                .Returns(new List<DocumentTypeEntity> { new DocumentTypeEntity
                {
                    DocumentTypeId = Guid.NewGuid(),
                    DocumentType= "Cedula"
                }});
            var service = new ServiceCollection();
            service.AddTransient(_ => providerRepoMock.Object);
            service.AddTransient(_ => DocumentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var providerSvc = provider.GetRequiredService<IProviderService>();

            var newProvider = new ProviderDto
            {
                DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0597"),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                PersonBusinessName = "OtherNameFake"
            };
            var response = await Assert.ThrowsAsync<CannotBeNaturalPersonException>(() =>
                providerSvc.AddProvider(newProvider)).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.Equal("Una persona no puede tener un tipo de documento diferente a Nit", response.Message);
        }

        [Fact]
        [UnitTest]
        public async Task Add_provider_successfull()
        {
            var DocumentTypeRepoMock = new Mock<IDocumentTypeRepository>();
            DocumentTypeRepoMock
                .Setup(m => m.SearchMatching(It.IsAny<Expression<Func<DocumentTypeEntity, bool>>>()))
                .Returns(new List<DocumentTypeEntity> { new DocumentTypeEntity
                {
                    DocumentTypeId = Guid.NewGuid(),
                    DocumentType= "Nit"
                }});
            var providerRepoMock = new Mock<IProviderRepository>();
            providerRepoMock
                .Setup(m => m.GetAll<ProviderEntity>())
                .Returns(new List<ProviderEntity> { new ProviderEntity
                {
                   DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0598"),
                   IdentificationNumber = 183,
                   PersonName="Juanito",
                   DocumentType = new DocumentTypeEntity { DocumentType = "Nit"},
                   PersonType = PersonType.NaturalPerson,
                   PersonBusinessName = "NameFake"
                }});
            providerRepoMock
                .Setup(emok => emok.Insert(It.IsAny<ProviderEntity>()))
                .Returns(() =>
                {
                    return Task.FromResult(new ProviderEntity
                    {
                        ProviderId = Guid.NewGuid()
                    });
                });
            var service = new ServiceCollection();
            service.AddTransient(_ => providerRepoMock.Object);
            service.AddTransient(_ => DocumentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var providerSvc = provider.GetRequiredService<IProviderService>();

            var newProvider = new ProviderDto
            {
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0597"),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                PersonType = PersonType.NaturalPerson,
                PersonBusinessName = "OtherNameFake"
            };

            var response = await providerSvc.AddProvider(newProvider).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.NotEqual(default, response);
        }

        [Fact]
        [IntegrationTest]
        public async Task Add_and_delete_Customer_Successfull_IntegrationTest()
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
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                PersonPhoneNumber = 3212224534,
                PersonEmail = "Fake@gmail.com",
                PersonBusinessName = "NameFake"
            };
            var responseAddProvider = await providerSvc.AddProvider(newProvider).ConfigureAwait(false);
            var providerDelete = new ProviderDto
            {
                ProviderId = Guid.Parse(responseAddProvider.ToString())
            };
            var responseDeleteProvider = providerSvc.DeleteProvider(providerDelete);
            var responseDeleteDocumentType = documentTypeSvc.DeleteDocumentType(newDocumentType);

            Assert.NotNull(responseAddDocumentType);
            Assert.NotEqual(default, responseAddDocumentType);
            Assert.NotEqual(default, responseAddProvider);
            Assert.True(responseDeleteDocumentType);
            Assert.True(responseDeleteProvider);
        }
        private static ServiceProvider GetProviderWithoutMock()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            return provider;
        }
    }
}
