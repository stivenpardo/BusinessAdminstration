using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.Employed.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Area;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.DocumentType;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Employed;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using BusinessAdministration.Domain.Core.PeopleManagement;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using BusinessAdministration.Domain.Core.PeopleManagement.Employed;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace BusinessAdministration.Test.Core._3.Application.Core.PeopleManagement.Employed
{
    public class AddEmployedTest
    {
        [Fact]
        [UnitTest]
        public async Task AddEmployed_Throw_Exception_when_properties_requires_are_null_or_empty()
        {
            var provider = GetProviderWithoutMock();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            await Assert.ThrowsAsync<EmployedCodeNotDefinedException>(() => employedSvc.AddEmployed(new EmployedDto
            {
                EmployedCode = default,
                AreaId = Guid.NewGuid(),
                DocumentTypeId = Guid.NewGuid(),
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
            })).ConfigureAwait(false);

            await Assert.ThrowsAsync<AreaIdNotDefinedException>(() => employedSvc.AddEmployed(new EmployedDto
            {
                EmployedCode = EmployedCode.Mg,
                AreaId = Guid.Empty,
                DocumentTypeId = Guid.NewGuid(),
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
            })).ConfigureAwait(false);

            await Assert.ThrowsAsync<DocumentTypeIdNotDefinedException>(() => employedSvc.AddEmployed(new EmployedDto
            {
                EmployedCode = EmployedCode.Mg,
                AreaId = Guid.NewGuid(),
                DocumentTypeId = Guid.Empty,
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
            })).ConfigureAwait(false);

            await Assert.ThrowsAsync<DateOfBirthNotDefinedException>(() => employedSvc.AddEmployed(new EmployedDto
            {
                EmployedCode = EmployedCode.Mg,
                AreaId = Guid.NewGuid(),
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
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            await Assert.ThrowsAsync<CreationDateNotDefinedException>(() => employedSvc.AddEmployed(new EmployedDto
            {
                EmployedCode = EmployedCode.Mg,
                AreaId = Guid.NewGuid(),
                DocumentTypeId = Guid.NewGuid(),
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = default,
            })).ConfigureAwait(false);
        }

        [Fact]
        [UnitTest]
        public async Task Throw_AlreadyExistException_when_there_are_two_persons_with_same_Name()
        {
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
                .Setup(m => m.GetAll<EmployedEntity>())
                .Returns(new List<EmployedEntity> { new EmployedEntity
                {
                   EmployedCode = EmployedCode.Mg,
                   AreaId = Guid.NewGuid(),
                   PersonName = "Pepito"
                }});

            var service = new ServiceCollection();
            service.AddTransient(_ => employedRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            var newEmployed = new EmployedDto
            {
                EmployedCode = EmployedCode.Mg,
                AreaId = Guid.NewGuid(),
                DocumentTypeId = Guid.NewGuid(),
                IdentificationNumber = 123,
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                PersonName = "Pepito"
            };
            var response = await Assert.ThrowsAsync<AlreadyExistException>(() =>
                employedSvc.AddEmployed(newEmployed)).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.Equal($"ya existe alguien con el nombre:  {newEmployed.PersonName}", response.Message);
        }
        [Fact]
        [UnitTest]
        public async Task Throw_AlreadyExistException_when_there_are_two_persons_with_same_IdentificationNumber_and_typeDocument()
        {
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
                .Setup(m => m.GetAll<EmployedEntity>())
                .Returns(new List<EmployedEntity> { new EmployedEntity
                {
                   EmployedCode = EmployedCode.Mg,
                   AreaId = Guid.NewGuid(),
                   DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0592"),
                   IdentificationNumber = 123,
                   PersonName="Juanito"
                }});

            var service = new ServiceCollection();
            service.AddTransient(_ => employedRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            var newEmployed = new EmployedDto
            {
                EmployedCode = EmployedCode.Mg,
                AreaId = Guid.NewGuid(),
                DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0592"),
                IdentificationNumber = 123,
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
            };
            var response = await Assert.ThrowsAsync<AlreadyExistException>(() =>
                employedSvc.AddEmployed(newEmployed)).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.Equal($"ya existe alguien el mismo numero de indentificación y tipo de documento: { newEmployed.IdentificationNumber}", response.Message);
        }

        [Fact]
        [UnitTest]
        public async Task Throws_CannotBeCorporatePersonException_when_Employed_is_have_document_type_NIT()
        {
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
                .Setup(m => m.GetAll<EmployedEntity>())
                .Returns(new List<EmployedEntity> { new EmployedEntity
                {
                   EmployedCode = EmployedCode.Mg,
                   AreaId = Guid.NewGuid(),
                   DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0592"),
                   IdentificationNumber = 183,
                   PersonName="Juanito",
                   DocumentType = new DocumentTypeEntity { DocumentType = "Nit"},
                }});
            var service = new ServiceCollection();
            service.AddTransient(_ => employedRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            var newEmployed = new EmployedDto
            {
                EmployedCode = EmployedCode.Mg,
                AreaId = Guid.NewGuid(),
                DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0597"),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                DocumentType = "Nit"

            };
            var response = await Assert.ThrowsAsync<CannotBeCorporatePersonException>(() =>
                employedSvc.AddEmployed(newEmployed)).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.Equal($"Una persona no puede tener un tipo de documento: { newEmployed.DocumentType}", response.Message);
        }

        [Fact]
        [UnitTest]
        public async Task Throw_CannotBeCorporatePerson_when_Employed_is_CorporatePerson()
        {
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
                .Setup(m => m.GetAll<EmployedEntity>())
                .Returns(new List<EmployedEntity> { new EmployedEntity
                {
                   EmployedCode = EmployedCode.Mg,
                   AreaId = Guid.NewGuid(),
                   DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0598"),
                   IdentificationNumber = 183,
                   PersonName="Juanito",
                   DocumentType = new DocumentTypeEntity { DocumentType = "Cédula"},
                   PersonType = PersonType.CorporatePerson

                }});
            var service = new ServiceCollection();
            service.AddTransient(_ => employedRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            var newEmployed = new EmployedDto
            {
                EmployedCode = EmployedCode.Mg,
                AreaId = Guid.NewGuid(),
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0597"),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                DocumentType = "Nit",
                PersonType = PersonType.CorporatePerson
            };
            var response = await Assert.ThrowsAsync<CannotBeCorporatePersonException>(() =>
                employedSvc.AddEmployed(newEmployed)).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.Equal($"Una empleado no puede ser: { newEmployed.PersonType}", response.Message);
        }

        //[Fact]
        //[UnitTest]
        //public async Task Throw_CannotBeCorporatePerson_when_Employed_Dont_have_unic_code()
        //{
        //    var employedRepoMock = new Mock<IEmployedRepository>();
        //    employedRepoMock
        //        .Setup(m => m.GetAll<EmployedEntity>())
        //        .Returns(new List<EmployedEntity> { new EmployedEntity
        //        {
        //           EmployedCode = EmployedCode.Mg,
        //           AreaId = Guid.NewGuid(),
        //           DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0598"),
        //           IdentificationNumber = 183,
        //           PersonName="Juanito",
        //           DocumentType = new DocumentTypeEntity { DocumentType = "Cédula"},
        //           PersonType = PersonType.CorporatePerson

        //        }});
        //    var service = new ServiceCollection();
        //    service.AddTransient(_ => employedRepoMock.Object);
        //    service.ConfigurePeopleManagementService(new DbSettings());
        //    var provider = service.BuildServiceProvider();
        //    var employedSvc = provider.GetRequiredService<IEmployedService>();

        //    var newEmployed = new EmployedDto
        //    {
        //        EmployedCode = EmployedCode.Mg,
        //        AreaId = Guid.NewGuid(),
        //        PersonDateOfBirth = DateTimeOffset.Now,
        //        CreationDate = DateTimeOffset.Now,
        //        DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0597"),
        //        IdentificationNumber = 123,
        //        PersonName = "Juanita",
        //        DocumentType = "Nit",
        //        PersonType = PersonType.NaturalPerson
        //    };
        //    var response = await Assert.ThrowsAsync<CannotBeCorporatePersonException>(() =>
        //        employedSvc.AddEmployed(newEmployed)).ConfigureAwait(false);
        //    Assert.NotNull(response);
        //    Assert.Equal($"Una empleado no puede ser: { newEmployed.PersonType}", response.Message);
        //}


        //[Fact]
        //[IntegrationTest]
        //public async Task AddArea_Successfull_IntegrationTest()
        //{
        //    //Todo: Creat Empleado, y elimnarlo al final para que no deje basura en la base de datos, igualmente para la entidad de area
        //    // tamnien llamar a los demas metodos como eliminar y actualizar
        //    var service = new ServiceCollection();
        //    service.ConfigurePeopleManagementService(new DbSettings
        //    {
        //        ConnectionString = "Data Source=DESKTOP-A52QQCF\\SQLEXPRESS;Initial Catalog=BusinessAdministration;Integrated Security=True"
        //    });
        //    var provider = service.BuildServiceProvider();
        //    var employedSvc = provider.GetRequiredService<IEmployedService>();

        //    var newEmployed = new EmployedDto
        //    {
        //        AreaName = "Fake area",
        //        LiableEmployerId = Guid.Parse("6b499387-b805-4339-8e8b-2d8bb08ba4eb")
        //    };
        //    var response = await employedSvc.AddArea(newEmployed).ConfigureAwait(false);

        //    Assert.NotNull(response);
        //    Assert.NotEqual(default, response);
        //}
        private static ServiceProvider GetProviderWithoutMock()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            return provider;
        }
    }
}
