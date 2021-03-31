using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.Customer.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.DocumentType;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person;
using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using BusinessAdministration.Domain.Core.PeopleManagement;
using BusinessAdministration.Domain.Core.PeopleManagement.Customer;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace BusinessAdministration.Test.Core._3.Application.Core.PeopleManagement.Customer
{
    public class AddCustomerTest
    {
        [Fact]
        [UnitTest]
        public async Task AddCustomer_Throw_Exception_when_properties_requires_are_null_or_empty()
        {
            var provider = GetProviderWithoutMock();
            var customerSvc = provider.GetRequiredService<ICustomerService>();

            await Assert.ThrowsAsync<DocumentTypeIdNotDefinedException>(() => customerSvc.AddCustomer(new CustomerDto
            {
                DocumentTypeId = Guid.Empty,
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
            })).ConfigureAwait(false);

            await Assert.ThrowsAsync<DateOfBirthNotDefinedException>(() => customerSvc.AddCustomer(new CustomerDto
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
            var customerSvc = provider.GetRequiredService<ICustomerService>();

            await Assert.ThrowsAsync<CreationDateNotDefinedException>(() => customerSvc.AddCustomer(new CustomerDto
            {
                DocumentTypeId = Guid.NewGuid(),
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = default,
            })).ConfigureAwait(false);
        }

        [Fact]
        [UnitTest]
        public async Task Throw_AlreadyExistException_when_there_are_two_persons_with_same_Name()
        {
            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                .Setup(m => m.GetAll<CustomerEntity>())
                .Returns(new List<CustomerEntity> { new CustomerEntity
                {
                   CustomerId = Guid.NewGuid(),
                   PersonName = "Pepito"
                }});

            var service = new ServiceCollection();
            service.AddTransient(_ => customerRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var customerSvc = provider.GetRequiredService<ICustomerService>();

            var newCustomer = new CustomerDto
            {
                DocumentTypeId = Guid.NewGuid(),
                IdentificationNumber = 123,
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                PersonName = "Pepito"
            };
            var response = await Assert.ThrowsAsync<AlreadyExistException>(() =>
                customerSvc.AddCustomer(newCustomer)).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.Equal($"ya existe alguien con el nombre:  {newCustomer.PersonName}", response.Message);
        }
        [Fact]
        [UnitTest]
        public async Task Throw_AlreadyExistException_when_there_are_two_persons_with_same_IdentificationNumber_and_typeDocument()
        {
            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                .Setup(m => m.GetAll<CustomerEntity>())
                .Returns(new List<CustomerEntity> { new CustomerEntity
                {
                   DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0592"),
                   IdentificationNumber = 123,
                   PersonName="Juanito"
                }});

            var service = new ServiceCollection();
            service.AddTransient(_ => customerRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var customerSvc = provider.GetRequiredService<ICustomerService>();

            var newCustomer = new CustomerDto
            {
                DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0592"),
                IdentificationNumber = 123,
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
            };
            var response = await Assert.ThrowsAsync<AlreadyExistException>(() =>
                customerSvc.AddCustomer(newCustomer)).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.Equal($"ya existe alguien el mismo numero de indentificación y tipo de documento: { newCustomer.IdentificationNumber}", response.Message);
        }

        [Fact]
        [UnitTest]
        public async Task Throws_NoExistDocumentTypeException_when_DoncumentType_dont_exist()
        {
            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                .Setup(m => m.GetAll<CustomerEntity>())
                .Returns(new List<CustomerEntity> { new CustomerEntity
                {
                   DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0592"),
                   IdentificationNumber = 183,
                   PersonName="Juanito",
                   DocumentType = new DocumentTypeEntity { DocumentType = "Nit"},
                }});
            var DocumentTypeRepoMock = new Mock<IDocumentTypeRepository>();
            DocumentTypeRepoMock
                .Setup(m => m.SearchMatching(It.IsAny<Expression<Func<DocumentTypeEntity, bool>>>()))
                .Returns(new List<DocumentTypeEntity>());

            var service = new ServiceCollection();
            service.AddTransient(_ => customerRepoMock.Object);
            service.AddTransient(_ => DocumentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var customerSvc = provider.GetRequiredService<ICustomerService>();

            var newCustomer = new CustomerDto
            {
                DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0597"),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
            };
            var response = await Assert.ThrowsAsync<NoExistDocumentTypeException>(() =>
                customerSvc.AddCustomer(newCustomer)).ConfigureAwait(false);
        }

        [Fact]
        [UnitTest]
        public async Task Throws_CannotBeCorporatePersonException_when_Person_is_have_document_type_NIT()
        {
            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                .Setup(m => m.GetAll<CustomerEntity>())
                .Returns(new List<CustomerEntity> { new CustomerEntity
                {
                   DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0592"),
                   IdentificationNumber = 183,
                   PersonName="Juanito",
                   DocumentType = new DocumentTypeEntity { DocumentType = "Nit"},
                }});
            var DocumentTypeRepoMock = new Mock<IDocumentTypeRepository>();
            DocumentTypeRepoMock
                .Setup(m => m.SearchMatching(It.IsAny<Expression<Func<DocumentTypeEntity, bool>>>()))
                .Returns(new List<DocumentTypeEntity> { new DocumentTypeEntity
                {
                    DocumentTypeId = Guid.NewGuid(),
                    DocumentType= "Nit"
                }});
            var service = new ServiceCollection();
            service.AddTransient(_ => customerRepoMock.Object);
            service.AddTransient(_ => DocumentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var customerSvc = provider.GetRequiredService<ICustomerService>();

            var newCustomer = new CustomerDto
            {
                DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0597"),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
            };
            var response = await Assert.ThrowsAsync<CannotBeCorporatePersonException>(() =>
                customerSvc.AddCustomer(newCustomer)).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.Equal("Una persona no puede tener un tipo de documento Nit", response.Message);
        }

        [Fact]
        [UnitTest]
        public async Task Add_customer_successfull()
        {
            var DocumentTypeRepoMock = new Mock<IDocumentTypeRepository>();
            DocumentTypeRepoMock
                .Setup(m => m.SearchMatching(It.IsAny<Expression<Func<DocumentTypeEntity, bool>>>()))
                .Returns(new List<DocumentTypeEntity> { new DocumentTypeEntity
                {
                    DocumentTypeId = Guid.NewGuid(),
                    DocumentType= "Cédula"
                }});
            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                .Setup(m => m.GetAll<CustomerEntity>())
                .Returns(new List<CustomerEntity> { new CustomerEntity
                {
                   DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0598"),
                   IdentificationNumber = 183,
                   PersonName="Juanito",
                   DocumentType = new DocumentTypeEntity { DocumentType = "Cédula"},
                   PersonType = PersonType.NaturalPerson,
                }});
            customerRepoMock
                .Setup(emok => emok.Insert(It.IsAny<CustomerEntity>()))
                .Returns(() =>
                {
                    return Task.FromResult(new CustomerEntity
                    {
                        CustomerId = Guid.NewGuid()
                    });
                });
            var service = new ServiceCollection();
            service.AddTransient(_ => customerRepoMock.Object);
            service.AddTransient(_ => DocumentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var customerSvc = provider.GetRequiredService<ICustomerService>();

            var newCustomer = new CustomerDto
            {
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0597"),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                PersonType = PersonType.NaturalPerson,
            };

            var response = await customerSvc.AddCustomer(newCustomer).ConfigureAwait(false);
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
            var customerSvc = provider.GetRequiredService<ICustomerService>();
            var documentTypeSvc = provider.GetRequiredService<IDocumentTypeService>();

            var newDocumentType = new DocumentTypeDto
            {
                DocumentTypeId = Guid.NewGuid(),
                DocumentType = "PasaportefaKeeee"
            };
            var responseAddDocumentType = await documentTypeSvc.AddDocumentType(newDocumentType).ConfigureAwait(false);
            var newCustomer = new CustomerDto
            {
                PersonType = PersonType.NaturalPerson,
                DocumentTypeId = Guid.Parse(responseAddDocumentType.ToString()),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                PersonLastName = "lastName fake",
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                PersonPhoneNumber = 3212224534,
                PersonEmail = "Fake@gmail.com"
            };
            var responseAddCustomer = await customerSvc.AddCustomer(newCustomer).ConfigureAwait(false);
            var customerDelete = new CustomerDto
            {
                CustomerId = Guid.Parse(responseAddCustomer.ToString())
            };
            var responseDeleteCustomer = customerSvc.DeleteCustomer(customerDelete);
            var responseDeleteDocumentType = documentTypeSvc.DeleteDocumentType(newDocumentType);

            Assert.NotNull(responseAddDocumentType);
            Assert.NotEqual(default, responseAddDocumentType);
            Assert.NotEqual(default, responseAddCustomer);
            Assert.True(responseDeleteDocumentType);
            Assert.True(responseDeleteCustomer);
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
