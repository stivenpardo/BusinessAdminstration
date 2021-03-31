using BusinessAdministration.Aplication.Core.PeopleManagement.Area.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Employed.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
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
    public class UpdateEmployedTest
    {
        [Fact]
        [UnitTest]
        public void UpdateEmployed_Throw_Exception_when_EmployedId_is_null_or_empty()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            Assert.Throws<IdCannotNullOrEmptyException>(() => employedSvc.UpdateEmployed(new EmployedDto
            {
                EmployedId= Guid.Empty,
                EmployedCode= Guid.NewGuid(),
            }));
        }
        [Fact]
        [UnitTest]
        public void Throw_DontExistIdException_when_id_it_isnt()
        {
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
                 .Setup(x => x.SearchMatching(It.IsAny<Expression<Func<EmployedEntity, bool>>>()))
                 .Returns(new List<EmployedEntity>());
            var service = new ServiceCollection();
            service.AddTransient(_ => employedRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            var newEmployed = new EmployedDto
            {
                EmployedId = Guid.NewGuid(),
                EmployedCode = Guid.NewGuid()
            };
            Assert.Throws<DontExistIdException>(() => employedSvc.UpdateEmployed(newEmployed));
        }

        [Fact]
        [UnitTest]
        public void UpdateEmployed_Successfult_Test()
        {
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
               .Setup(x => x.SearchMatching(It.IsAny<Expression<Func<EmployedEntity, bool>>>()))
               .Returns(new List<EmployedEntity> { new EmployedEntity
               {
                   EmployedId= Guid.NewGuid()
               }});
            employedRepoMock
                 .Setup(x => x.Update(It.IsAny<EmployedEntity>()))
                 .Returns(() =>
                 {
                     return true;
                 });
            var service = new ServiceCollection();
            service.AddTransient(_ => employedRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            var newEmployed = new EmployedDto
            {
                EmployedId = Guid.NewGuid(),
                PersonName = "NAME FAKE"
            };
            var response = employedSvc.UpdateEmployed(newEmployed);
            Assert.NotEqual(default, response);
            Assert.True(response);
        }
        [Fact]
        [IntegrationTest]
        public async Task UpdateEmployed_Successfull_IntegrationTest()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings
            {
                ConnectionString = "Data Source=DESKTOP-A52QQCF\\SQLEXPRESS;Initial Catalog=BusinessAdministration;Integrated Security=True"
            });
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();
            var documentTypeSvc = provider.GetRequiredService<IDocumentTypeService>();
            var areaSvc = provider.GetRequiredService<IAreaService>();

            var newDocumentType = new DocumentTypeDto
            {
                DocumentTypeId = Guid.NewGuid(),
                DocumentType = "PasaportefaKeeee"
            };
            var responseAddDocumentType = await documentTypeSvc.AddDocumentType(newDocumentType).ConfigureAwait(false);
            var newArea = new AreaRequestDto
            {
                AreaName = "Fake area"
            };
            var responseAddArea = await areaSvc.AddArea(newArea).ConfigureAwait(false);
            var newEmployed = new EmployedDto
            {
                EmployedId = Guid.NewGuid(),
                EmployedCode = Guid.NewGuid(),
                PersonType = PersonType.NaturalPerson,
                EmployedPosition = EmployedPosition.Developer,
                AreaId = Guid.Parse(responseAddArea.ToString()),
                DocumentTypeId = Guid.Parse(responseAddDocumentType.ToString()),
                IdentificationNumber = 123,
                PersonName = "Juanita",
                PersonLastName = "lastName fake",
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                PersonPhoneNumber = 3212224534,
                PersonEmail = "Fake@gmail.com"
            };
            var responseAddEmployed = await employedSvc.AddEmployed(newEmployed).ConfigureAwait(false);
            var updateEmployed = new EmployedDto
            {
                EmployedId = Guid.Parse(responseAddEmployed.ToString()),
                EmployedCode = Guid.NewGuid(),
                PersonType = PersonType.NaturalPerson,
                EmployedPosition = EmployedPosition.Manager,
                AreaId = Guid.Parse(responseAddArea.ToString()),
                DocumentTypeId = Guid.Parse(responseAddDocumentType.ToString()),
                IdentificationNumber = 133,
                PersonName = "updateName fake",
                PersonLastName = "update fake",
                PersonDateOfBirth = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                PersonPhoneNumber = 3212224534,
                PersonEmail = "Fake-update@gmail.com"
            };
            var responseUpdateEmployed = employedSvc.UpdateEmployed(updateEmployed);
            var employedDelete = new EmployedRequestDto
            {
                EmployedId = Guid.Parse(responseAddEmployed.ToString())
            };
            var responseDeleteEmployed = employedSvc.DeleteEmployed(employedDelete);
            var responseDeleteDocumentType = documentTypeSvc.DeleteDocumentType(newDocumentType);
            var areaDelete = new AreaDto
            {
                AreaId = Guid.Parse(responseAddArea.ToString())
            };
            var responseDeleArea = areaSvc.DeleteArea(areaDelete);

            Assert.NotNull(responseAddDocumentType);
            Assert.NotNull(responseAddArea);
            Assert.NotEqual(default, responseAddDocumentType);
            Assert.NotEqual(default, responseAddArea);
            Assert.NotEqual(default, responseAddEmployed);
            Assert.True(responseUpdateEmployed);
            Assert.True(responseDeleteDocumentType);
            Assert.True(responseDeleArea);
            Assert.True(responseDeleteEmployed);
        }
    }

}

