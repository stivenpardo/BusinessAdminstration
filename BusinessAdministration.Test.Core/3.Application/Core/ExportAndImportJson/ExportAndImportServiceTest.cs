using BusinessAdministration.Aplication.Core.ExportAndImportJSON;
using BusinessAdministration.Aplication.Core.ExportAndImportJSON.Configuration;
using BusinessAdministration.Aplication.Core.ExportAndImportJSON.Exceptions;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using Microsoft.Extensions.DependencyInjection;
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
        //[Fact]
        //[UnitTest]
        //public async Task Export_Full()
        //{
        //    var service = new ServiceCollection();

        //    service.ConfigureExportAndImportJson();
        //    var provider = service.BuildServiceProvider();
        //    var ExportAndImportJson = provider.GetRequiredService<IExportAndImportJson>();

        //    var dtoCustomer = new CustomerDto
        //    {
        //        Id = Guid.NewGuid(),
        //        Nombre = "IntegracionPersona",
        //        Apellido = "IntegracionPersona",
        //        FechaNacimiento = DateTimeOffset.Now,
        //        FechaRegistro = DateTimeOffset.Now,
        //        NumeroTelefono = 123456789,
        //        CorreoElectronico = "fake@fake.fake",
        //    };

        //    var result = await ExportAndImportJson.ExportJson("ExportCliente", new List<CustomerDto> { dtoCustomer }).ConfigureAwait(false);
        //    var dtoCustomerList = new List<CustomerDto>{ new CustomerDto
        //    {
        //        Id = Guid.NewGuid(),
        //        Nombre = "IntegracionPersona",
        //        Apellido = "IntegracionPersona",
        //        FechaNacimiento = DateTimeOffset.Now,
        //        FechaRegistro = DateTimeOffset.Now,
        //        NumeroTelefono = 123456789,
        //        CorreoElectronico = "fake@fake.fake",
        //    },
        //    new CustomerDto
        //    {
        //        Id = Guid.NewGuid(),
        //        Nombre = "IntegracionPersona",
        //        Apellido = "IntegracionPersona",
        //        FechaNacimiento = DateTimeOffset.Now,
        //        FechaRegistro = DateTimeOffset.Now,
        //        NumeroTelefono = 123456789,
        //        CorreoElectronico = "fake@fake.fake",
        //    }};
        //    var result2 = await ExportAndImportJson.ExportJson("ExportListCliente", dtoCustomerList).ConfigureAwait(false);

        //    Assert.NotNull(result);
        //    Assert.NotNull(result2);
        //}
        //[Fact]
        //[UnitTest]
        //public async Task Import_Full()
        //{
        //    var service = new ServiceCollection();

        //    service.ConfigureExportAndImportJson();
        //    var provider = service.BuildServiceProvider();
        //    var ExportAndImportJson = provider.GetRequiredService<IExportAndImportJson>();

        //    var result = await ExportAndImportJson.ImportJson<IEnumerable<CustomerDto>>("ExportCliente").ConfigureAwait(false);
        //    var result2 = await ExportAndImportJson.ImportJson<IEnumerable<CustomerDto>>("ExportListCliente").ConfigureAwait(false);

        //    Assert.NotNull(result);
        //    Assert.NotNull(result2);
        //}
    }
}
