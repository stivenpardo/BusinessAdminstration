using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.Employed.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Area;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.DocumentType;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Employed;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using BusinessAdministration.Domain.Core.PeopleManagement;
using BusinessAdministration.Domain.Core.PeopleManagement.Area;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using BusinessAdministration.Domain.Core.PeopleManagement.Employed;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
                EmployedCode = Guid.NewGuid(),
                AreaId = Guid.Empty,
                DocumentTypeId = Guid.NewGuid(),
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
            })).ConfigureAwait(false);

            await Assert.ThrowsAsync<DocumentTypeIdNotDefinedException>(() => employedSvc.AddEmployed(new EmployedDto
            {
                EmployedCode = Guid.NewGuid(),
                AreaId = Guid.NewGuid(),
                DocumentTypeId = Guid.Empty,
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
            })).ConfigureAwait(false);

            await Assert.ThrowsAsync<DateOfBirthNotDefinedException>(() => employedSvc.AddEmployed(new EmployedDto
            {
                EmployedCode = Guid.NewGuid(),
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
                EmployedCode = Guid.NewGuid(),
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
                   EmployedCode = Guid.NewGuid(),
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
                EmployedCode = Guid.NewGuid(),
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
                   EmployedCode = Guid.NewGuid(),
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
                EmployedCode = Guid.NewGuid(),
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
        public async Task Throws_NoExistDocumentTypeException_when_DoncumentType_dont_exist()
        {
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
                .Setup(m => m.GetAll<EmployedEntity>())
                .Returns(new List<EmployedEntity> { new EmployedEntity
                {
                   EmployedCode = Guid.NewGuid(),
                   AreaId = Guid.NewGuid(),
                   DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0592"),
                   IdentificationNumber = 183,
                   PersonName="Juanito",
                   DocumentType = new DocumentTypeEntity { DocumentType = "Nit"},
                }});
            var DocumentTypeRepoMock = new Mock<IDocumentTypeRepository>();
            DocumentTypeRepoMock
                .Setup(m => m.SearchMatching(It.IsAny<Expression<Func<DocumentTypeEntity, bool>>>()))
                .Returns(new List<DocumentTypeEntity> ());

            var service = new ServiceCollection();
            service.AddTransient(_ => employedRepoMock.Object);
            service.AddTransient(_ => DocumentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            var newEmployed = new EmployedDto
            {
                EmployedCode = Guid.NewGuid(),
                AreaId = Guid.NewGuid(),
                DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0597"),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
            };
            var response = await Assert.ThrowsAsync<NoExistDocumentTypeException>(() =>
                employedSvc.AddEmployed(newEmployed)).ConfigureAwait(false);
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
                   EmployedCode = Guid.NewGuid(),
                   AreaId = Guid.NewGuid(),
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
            service.AddTransient(_ => employedRepoMock.Object);
            service.AddTransient(_ => DocumentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            var newEmployed = new EmployedDto
            {
                EmployedCode = Guid.NewGuid(),
                AreaId = Guid.NewGuid(),
                DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0597"),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
            };
            var response = await Assert.ThrowsAsync<CannotBeCorporatePersonException>(() =>
                employedSvc.AddEmployed(newEmployed)).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.Equal("Una persona no puede tener un tipo de documento Nit", response.Message);
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
                   EmployedCode = Guid.NewGuid(),
                   AreaId = Guid.NewGuid(),
                   DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0598"),
                   IdentificationNumber = 183,
                   PersonName="Juanito",
                   DocumentType = new DocumentTypeEntity { DocumentType = "Cédula"},
                   PersonType = PersonType.CorporatePerson

                }});
            var DocumentTypeRepoMock = new Mock<IDocumentTypeRepository>();
            DocumentTypeRepoMock
                .Setup(m => m.SearchMatching(It.IsAny<Expression<Func<DocumentTypeEntity, bool>>>()))
                .Returns(new List<DocumentTypeEntity> { new DocumentTypeEntity
                {
                    DocumentTypeId = Guid.NewGuid(),
                    DocumentType= "Cédula"
                }});
            var service = new ServiceCollection();
            service.AddTransient(_ => employedRepoMock.Object);
            service.AddTransient(_ => DocumentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            var newEmployed = new EmployedDto
            {
                EmployedCode = Guid.NewGuid(),
                AreaId = Guid.NewGuid(),
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0597"),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                PersonType = PersonType.CorporatePerson
            };
            var response = await Assert.ThrowsAsync<CannotBeCorporatePersonException>(() =>
                employedSvc.AddEmployed(newEmployed)).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.Equal($"Una empleado no puede ser: { newEmployed.PersonType}", response.Message);
        }

        [Fact]
        [UnitTest]
        public async Task Throw_CannotBeCorporatePerson_when_Employed_Dont_have_unic_code()
        {
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
                .Setup(m => m.GetAll<EmployedEntity>())
                .Returns(new List<EmployedEntity> { new EmployedEntity
                {
                   AreaId = Guid.NewGuid(),
                   DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0598"),
                   IdentificationNumber = 183,
                   PersonName="Juanito",
                   DocumentType = new DocumentTypeEntity { DocumentType = "Cédula"},
                   PersonType = PersonType.NaturalPerson,
                   EmployedCode = Guid.Parse("57c3aa7a-7e24-44db-89a8-711a75395160")

                }});
            var DocumentTypeRepoMock = new Mock<IDocumentTypeRepository>();
            DocumentTypeRepoMock
                .Setup(m => m.SearchMatching(It.IsAny<Expression<Func<DocumentTypeEntity, bool>>>()))
                .Returns(new List<DocumentTypeEntity> { new DocumentTypeEntity
                {
                    DocumentTypeId = Guid.NewGuid(),
                    DocumentType= "Cédula"
                }});
            var service = new ServiceCollection();
            service.AddTransient(_ => employedRepoMock.Object);
            service.AddTransient(_ => DocumentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            var newEmployed = new EmployedDto
            {
                AreaId = Guid.NewGuid(),
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0597"),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                PersonType = PersonType.NaturalPerson,
                EmployedCode = Guid.Parse("57c3aa7a-7e24-44db-89a8-711a75395160")
            };
            var response = await Assert.ThrowsAsync<CannotBeCorporatePersonException>(() =>
                employedSvc.AddEmployed(newEmployed)).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.Equal($"El empleado no tiene un código unnico: { newEmployed.EmployedCode}", response.Message);
        }

        [Fact]
        [UnitTest]
        public async Task Throw_NotExistAreaException_when_IdArea_Dont_Exist()
        {
            var areaRepoMock = new Mock<IAreaRepository>();
            areaRepoMock
                 .Setup(m => m.SearchMatching(It.IsAny<Expression<Func<AreaEntity, bool>>>()))
                 .Returns(new List<AreaEntity>());
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
                .Setup(m => m.GetAll<EmployedEntity>())
                .Returns(new List<EmployedEntity> { new EmployedEntity
                {
                   AreaId = Guid.NewGuid(),
                   DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0598"),
                   IdentificationNumber = 183,
                   PersonName="Juanito",
                   DocumentType = new DocumentTypeEntity { DocumentType = "Cédula"},
                   PersonType = PersonType.NaturalPerson,
                   EmployedCode = Guid.Parse("57c3aa7a-7e24-44db-89a8-711a75395160")

                }});
            var DocumentTypeRepoMock = new Mock<IDocumentTypeRepository>();
            DocumentTypeRepoMock
                .Setup(m => m.SearchMatching(It.IsAny<Expression<Func<DocumentTypeEntity, bool>>>()))
                .Returns(new List<DocumentTypeEntity> { new DocumentTypeEntity
                {
                    DocumentTypeId = Guid.NewGuid(),
                    DocumentType= "Cédula"
                }});
            var service = new ServiceCollection();
            service.AddTransient(_ => employedRepoMock.Object);
            service.AddTransient(_ => areaRepoMock.Object);
            service.AddTransient(_ => DocumentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            var newEmployed = new EmployedDto
            {
                AreaId = Guid.NewGuid(),
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0597"),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                PersonType = PersonType.NaturalPerson,
                EmployedCode = Guid.Parse("67c3aa7a-7e24-44db-89a8-711a75395160")
            };
            var response = await Assert.ThrowsAsync<NotExistAreaException>(() =>
                employedSvc.AddEmployed(newEmployed)).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.Equal($"No existe el area del siguiente Id: { newEmployed.AreaId}", response.Message);
        }

        [Fact]
        [UnitTest]
        public async Task Throw_AlreadyExistException_when_Area_already_was_assigned_to_employed()
        {
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
                .Setup(m => m.GetAll<EmployedEntity>())
                .Returns(new List<EmployedEntity> { new EmployedEntity
                {
                   AreaId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0598"),
                   DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0598"),
                   IdentificationNumber = 183,
                   PersonName="Juanito",
                   DocumentType = new DocumentTypeEntity { DocumentType = "Cédula"},
                   PersonType = PersonType.NaturalPerson,
                   EmployedCode = Guid.Parse("57c3aa7a-7e24-44db-89a8-711a75395161")

                }});
            var DocumentTypeRepoMock = new Mock<IDocumentTypeRepository>();
            DocumentTypeRepoMock
                .Setup(m => m.SearchMatching(It.IsAny<Expression<Func<DocumentTypeEntity, bool>>>()))
                .Returns(new List<DocumentTypeEntity> { new DocumentTypeEntity
                {
                    DocumentTypeId = Guid.NewGuid(),
                    DocumentType= "Cédula"
                }});
            var service = new ServiceCollection();
            service.AddTransient(_ => employedRepoMock.Object);
            service.AddTransient(_ => DocumentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            var newEmployed = new EmployedDto
            {
                AreaId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0598"),
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0597"),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                PersonType = PersonType.NaturalPerson,
                EmployedCode = Guid.Parse("57c3aa7a-7e24-44db-89a8-711a75395160")
            };
            var response = await Assert.ThrowsAsync<AlreadyExistException>(() =>
                employedSvc.AddEmployed(newEmployed)).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.Equal($"La area : { newEmployed.AreaId} ya fue asignada", response.Message);
        }

        [Fact]
        [UnitTest]
        public async Task Add_employed_successfull()
        {
            var areaRepoMock = new Mock<IAreaRepository>();
            areaRepoMock
                 .Setup(m => m.SearchMatching(It.IsAny<Expression<Func<AreaEntity, bool>>>()))
                 .Returns(new List<AreaEntity> { new AreaEntity
                 {
                     AreaId= Guid.NewGuid(),
                     AreaName = "fake area",
                 } });
            var DocumentTypeRepoMock = new Mock<IDocumentTypeRepository>();
            DocumentTypeRepoMock
                .Setup(m => m.SearchMatching(It.IsAny<Expression<Func<DocumentTypeEntity, bool>>>()))
                .Returns(new List<DocumentTypeEntity> { new DocumentTypeEntity
                {
                    DocumentTypeId = Guid.NewGuid(),
                    DocumentType= "Cédula"
                }});
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
                .Setup(m => m.GetAll<EmployedEntity>())
                .Returns(new List<EmployedEntity> { new EmployedEntity
                {
                   AreaId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0598"),
                   DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0598"),
                   IdentificationNumber = 183,
                   PersonName="Juanito",
                   DocumentType = new DocumentTypeEntity { DocumentType = "Cédula"},
                   PersonType = PersonType.NaturalPerson,
                   EmployedCode = Guid.Parse("57c3aa7a-7e24-44db-89a8-711a75395161")

                }});
            employedRepoMock
                .Setup(emok => emok.Insert(It.IsAny<EmployedEntity>()))
                .Returns(() => 
                {
                    return Task.FromResult(new EmployedEntity
                    {
                        EmployedId = Guid.NewGuid()
                    });                 
                });
            var service = new ServiceCollection();
            service.AddTransient(_ => employedRepoMock.Object);
            service.AddTransient(_ => areaRepoMock.Object);
            service.AddTransient(_ => DocumentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            var newEmployed = new EmployedDto
            {
                AreaId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0599"),
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0597"),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                PersonType = PersonType.NaturalPerson,
                EmployedCode = Guid.Parse("57c3aa7a-7e24-44db-89a8-711a75395160")
            };

            var response = await employedSvc.AddEmployed(newEmployed).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.NotEqual(default, response);
        }
        [Fact]
        [IntegrationTest]
        public async Task AddArea_Successfull_IntegrationTest()
        {
            //Todo: Crear Empleado, y elinarlo al final para que no deje basura en la base de datos, igualmente para la entidad de area
            // tamnien llamar a los demas metodos como eliminar y actualizar
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings
            {
                ConnectionString = "Data Source=DESKTOP-A52QQCF\\SQLEXPRESS;Initial Catalog=BusinessAdministration;Integrated Security=True"
            });
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            var newEmployed = new EmployedDto
            {
                EmployedId = Guid.NewGuid(),
                AreaId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0599"),
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                DocumentTypeId = Guid.Parse("ac620062-11b7-4a11-95c6-7825c68c0597"),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                PersonType = PersonType.NaturalPerson,
                EmployedCode = Guid.Parse("57c3aa7a-7e24-44db-89a8-711a75395160")

            };
            var response = await employedSvc.AddEmployed(newEmployed).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.NotEqual(default, response);
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
