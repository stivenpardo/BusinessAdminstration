using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.Customer.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person;
using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using BusinessAdministration.Domain.Core.PeopleManagement;
using BusinessAdministration.Domain.Core.PeopleManagement.Customer;
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
    public class UpdateCustomerTest
    {
        [Fact]
        [UnitTest]
        public void UpdateCustomer_Throw_Exception_when_CustomerId_is_null_or_empty()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var customerSvc = provider.GetRequiredService<ICustomerService>();

            Assert.Throws<IdCannotNullOrEmptyException>(() => customerSvc.UpdateCustomer(new CustomerDto
            {
                CustomerId = Guid.Empty,
                DocumentTypeId = Guid.NewGuid(),
            }));
        }
        [Fact]
        [UnitTest]
        public void Throw_DontExistIdException_when_id_it_isnt()
        {
            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                 .Setup(x => x.SearchMatching(It.IsAny<Expression<Func<CustomerEntity, bool>>>()))
                 .Returns(new List<CustomerEntity>());
            var service = new ServiceCollection();
            service.AddTransient(_ => customerRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var customerSvc = provider.GetRequiredService<ICustomerService>();

            var newCustomer = new CustomerDto
            {
                CustomerId = Guid.NewGuid(),
                DocumentTypeId = Guid.NewGuid()
            };
            Assert.Throws<DontExistIdException>(() => customerSvc.UpdateCustomer(newCustomer));
        }

        [Fact]
        [UnitTest]
        public void UpdateCustomer_Successfult_Test()
        {
            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
               .Setup(x => x.SearchMatching(It.IsAny<Expression<Func<CustomerEntity, bool>>>()))
               .Returns(new List<CustomerEntity> { new CustomerEntity
               {
                   CustomerId = Guid.NewGuid()
               }});
            customerRepoMock
                 .Setup(x => x.Update(It.IsAny<CustomerEntity>()))
                 .Returns(() =>
                 {
                     return true;
                 });
            var service = new ServiceCollection();
            service.AddTransient(_ => customerRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var customerSvc = provider.GetRequiredService<ICustomerService>();

            var newCustomer = new CustomerDto
            {
                CustomerId = Guid.NewGuid(),
                PersonName = "NAME FAKE"
            };
            var response = customerSvc.UpdateCustomer(newCustomer);
            Assert.NotEqual(default, response);
            Assert.True(response);
        }
        [Fact]
        [IntegrationTest]
        public async Task UpdateCustomer_Successfull_IntegrationTest()
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
                PersonName = "JuanitaPeres",
                PersonLastName = "lastName fake",
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                PersonPhoneNumber = 3212224534,
                PersonEmail = "Fake@gmail.com"
            };
            var responseAddCustomer = await customerSvc.AddCustomer(newCustomer).ConfigureAwait(false);
            var updateCustomer = new CustomerDto
            {
                CustomerId = Guid.Parse(responseAddCustomer.ToString()),
                PersonType = PersonType.NaturalPerson,
                DocumentTypeId = Guid.Parse(responseAddDocumentType.ToString()),
                IdentificationNumber = 133,
                PersonName = "updateName fake2",
                PersonLastName = "update fake",
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                PersonPhoneNumber = 3212224534,
                PersonEmail = "Fake-update@gmail.com"
            };
            var responseUpdateCustomer = customerSvc.UpdateCustomer(updateCustomer);
            var customerDelete = new CustomerDto
            {
                CustomerId = Guid.Parse(responseAddCustomer.ToString())
            };
            var responseDeleteCustomer = customerSvc.DeleteCustomer(customerDelete);
            var responseDeleteDocumentType = documentTypeSvc.DeleteDocumentType(newDocumentType);

            Assert.NotNull(responseAddDocumentType);
            Assert.NotEqual(default, responseAddDocumentType);
            Assert.NotEqual(default, responseAddCustomer);
            Assert.True(responseUpdateCustomer);
            Assert.True(responseDeleteDocumentType);
            Assert.True(responseDeleteCustomer);
        }
    }

}

