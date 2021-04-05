using BusinessAdministration.Aplication.Core.ExportAndImportJSON;
using BusinessAdministration.Aplication.Core.ExportAndImportJSON.Configuration;
using BusinessAdministration.Aplication.Core.ExportAndImportJSON.Exceptions;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using BusinessAdministration.Domain.Core.PeopleManagement;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace BusinessAdministration.Test.Core._3.Application.Core.ExportAndImportJson
{
    public class ExportAndImportServiceTest
    {
        [Fact]
        [UnitTest]
        public async Task Patch_Throws_NotImplementedException()
        {
            var service = new ServiceCollection();

            service.ConfigureExportAndImportJson();
            var provider = service.BuildServiceProvider();
            var ExportAndImportJson = provider.GetRequiredService<IExportAndImportJson>();

            var dtoCustomer = new List<CustomerDto>{ new CustomerDto
            {
                PersonName = "fakename",
                PersonLastName = "fakeLasname"
            }};
            await Assert.ThrowsAsync<NotDefinedPathException>(async () => await ExportAndImportJson.ExportJson("", dtoCustomer).ConfigureAwait(false)).ConfigureAwait(false);
        }
        [Fact]
        [UnitTest]
        public async Task Export_Full()
        {
            var service = new ServiceCollection();

            service.ConfigureExportAndImportJson();
            var provider = service.BuildServiceProvider();
            var ExportAndImportJson = provider.GetRequiredService<IExportAndImportJson>();

            var dtoCustomer = new CustomerDto
            {
                PersonType = PersonType.NaturalPerson,
                DocumentTypeId = Guid.NewGuid(),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                PersonLastName = "lastName fake",
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                PersonPhoneNumber = 3212224534,
                PersonEmail = "Fake@gmail.com"
            };

            var result = await ExportAndImportJson.ExportJson("ExportCliente", new List<CustomerDto> { dtoCustomer }).ConfigureAwait(false);
            var dtoCustomerList = new List<CustomerDto>{ new CustomerDto
            {
                PersonType = PersonType.NaturalPerson,
                DocumentTypeId = Guid.NewGuid(),
                IdentificationNumber = 123,
                PersonName = "Juanita1",
                PersonLastName = "lastName fake",
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                PersonPhoneNumber = 3212224534,
                PersonEmail = "Fake@gmail.com"
            },
            new CustomerDto
            {
                PersonType = PersonType.NaturalPerson,
                DocumentTypeId = Guid.NewGuid(),
                IdentificationNumber = 123,
                PersonName = "Juanita2",
                PersonLastName = "lastName fake",
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                PersonPhoneNumber = 3212224534,
                PersonEmail = "Fake@gmail.com"
            }};
            var result2 = await ExportAndImportJson.ExportJson("ExportListCliente", dtoCustomerList).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.NotNull(result2);
        }
        [Fact]
        [UnitTest]
        public async Task Import_Full()
        {
            var service = new ServiceCollection();

            service.ConfigureExportAndImportJson();
            var provider = service.BuildServiceProvider();
            var ExportAndImportJson = provider.GetRequiredService<IExportAndImportJson>();

            var result = await ExportAndImportJson.ImportJson<IEnumerable<CustomerDto>>("ExportCliente").ConfigureAwait(false);
            var result2 = await ExportAndImportJson.ImportJson<IEnumerable<CustomerDto>>("ExportListCliente").ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.NotNull(result2);
        }
    }
}
