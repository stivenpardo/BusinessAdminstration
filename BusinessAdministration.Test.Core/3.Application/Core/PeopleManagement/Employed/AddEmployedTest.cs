using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.Employed.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Area;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.DocumentType;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Employed;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using BusinessAdministration.Domain.Core.PeopleManagement;
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
        private static ServiceProvider GetProvider()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            return provider;
        }

        [Fact]
        [UnitTest]
        public async Task AddEmployed_Throw_Exception_when_properties_requires_are_null_or_empty()
        {
            var provider = GetProvider();
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
            //TODO: Implemented test for each one aceptation criterial for persons an employed 
            var provider = GetProvider();
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
        public async Task Throws_Expection_when_there_are_two_persons_with_same_IdentificationNumber_and_typeDocument()
        {
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
                .Setup(m => m.SearchMatching(It.IsAny<Expression<Func<EmployedEntity, bool>>>()))
                .Returns(new List<EmployedEntity> { new EmployedEntity
                {
                   
                }});

            var provider = GetProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            var newEmployed = new EmployedDto
            {
                EmployedCode = EmployedCode.Mg,
                AreaId = Guid.NewGuid(),
                DocumentTypeId = Guid.NewGuid(),
                IdentificationNumber = 123,
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
            };
            var response = await Assert.ThrowsAsync<AlreadyExistException>(() =>
                employedSvc.AddEmployed(newEmployed)).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.Equal(newEmployed.IdentificationNumber.ToString(), response.Message);
        }

        [Fact]
        [UnitTest]
        public async Task Throws_Expection_when_Employed_is_CorporatePerson()
        {
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
                .Setup(m => m.SearchMatching(It.IsAny<Expression<Func<EmployedEntity, bool>>>()))
                .Returns(new List<EmployedEntity>());

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
                PersonName = "pepito",
                PersonType = PersonType.CorporatePerson
            };
            var response = await Assert.ThrowsAsync<AlreadyExistException>(() =>
                employedSvc.AddEmployed(newEmployed)).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.Equal(newEmployed.IdentificationNumber.ToString(), response.Message);
        }

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
    }
}
