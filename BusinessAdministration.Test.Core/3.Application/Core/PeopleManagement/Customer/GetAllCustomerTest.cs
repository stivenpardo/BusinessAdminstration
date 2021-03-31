using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.Customer.Services;
using BusinessAdministration.Domain.Core.PeopleManagement.Customer;
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
    public class GetAllCustomerTest
    {
        [Fact]
        [UnitTest]
        public async Task Get_All_Successful()
        {
            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                .Setup(m => m.GetAll<CustomerEntity>())
                .Returns(new List<CustomerEntity> { new CustomerEntity
                {
                    CustomerId= Guid.NewGuid(),
                    DocumentTypeId= Guid.NewGuid(),
                },
                 new CustomerEntity
                {
                    CustomerId= Guid.NewGuid(),
                    DocumentTypeId= Guid.NewGuid(),
                }});
            var service = new ServiceCollection();
            service.AddTransient(_ => customerRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var customerSvc = provider.GetRequiredService<ICustomerService>();
            var response = await customerSvc.GetAll().ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.NotEqual(default, response);
        }
        [Fact]
        [IntegrationTest]
        public async Task GetAllCustomer_Successfull_IntegrationTest()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings
            {
                ConnectionString = "Data Source=DESKTOP-A52QQCF\\SQLEXPRESS;Initial Catalog=BusinessAdministration;Integrated Security=True"
            });
            var provider = service.BuildServiceProvider();
            var customerSvc = provider.GetRequiredService<ICustomerService>();
            var responseSearch = await customerSvc.GetAll().ConfigureAwait(false);
            Assert.NotEqual(default, responseSearch);
        }
    }
}
