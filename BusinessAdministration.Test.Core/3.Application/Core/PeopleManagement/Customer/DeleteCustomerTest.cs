using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.Customer.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using BusinessAdministration.Domain.Core.PeopleManagement.Customer;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;
using Xunit.Categories;

namespace BusinessAdministration.Test.Core._3.Application.Core.PeopleManagement.Customer
{
    public class DeleteCustomerTest
    {
        [Fact]
        [UnitTest]
        public void DeleteCustomer_Throw_IdCannotNullOrEmptyException_when_CustomerId_is_null_or_empty()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var customerSvc = provider.GetRequiredService<ICustomerService>();

            Assert.Throws<IdCannotNullOrEmptyException>(() => customerSvc.DeleteCustomer(new CustomerDto
            {
                CustomerId = Guid.Empty
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
            };
            Assert.Throws<DontExistIdException>(() => customerSvc.DeleteCustomer(newCustomer));
        }
        [Fact]
        [UnitTest]
        public void DeleteCustomer_Successfult_Test()
        {
            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                .Setup(e => e.SearchMatching(It.IsAny<Expression<Func<CustomerEntity, bool>>>()))
                .Returns(new List<CustomerEntity> { new CustomerEntity
                {
                    CustomerId = Guid.NewGuid()
                }});

            customerRepoMock
                .Setup(e => e.Delete(It.IsAny<CustomerEntity>()))
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
                CustomerId = Guid.Parse("31826538-6b06-4021-95c2-27fb184ac4fe")
            };

            var responseDelete = customerSvc.DeleteCustomer(newCustomer);
            Assert.NotEqual(default, responseDelete);
            Assert.True(responseDelete);
        }
    }
}
