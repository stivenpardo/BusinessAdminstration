using BusinessAdministration.Aplication.Core.PeopleManagement.Area.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Area;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using BusinessAdministration.Domain.Core.PeopleManagement.Area;
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

namespace BusinessAdministration.Test.Core._3.Application.Core.PeopleManagement.Area
{
    public class AddAreaTest
    {
        [Fact]
        [UnitTest]
        public async Task AddArea_Throw_Exception_when_properties_are_null_or_empty()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var areaSvc = provider.GetRequiredService<IAreaService>();

            await Assert.ThrowsAsync<AreaNameNotDefinedException>(() => areaSvc.AddArea(new AreaRequestDto
            {
                AreaName = string.Empty,
                LiableEmployerId = Guid.NewGuid()
            })).ConfigureAwait(false);

            await Assert.ThrowsAsync<AreaNameNotDefinedException>(() => areaSvc.AddArea(new AreaRequestDto
            {
                AreaName = null,
                LiableEmployerId = Guid.NewGuid()
            })).ConfigureAwait(false);
        }
        [Fact]
        [UnitTest]
        public async Task AddArea_Successfult_When_LiableEmployed_is_null_Test()
        {
            var areaRepoMock = new Mock<IAreaRepository>();
            areaRepoMock
                 .Setup(x => x.Insert(It.IsAny<AreaEntity>()))
                 .Returns(() =>
                 {
                     return Task.FromResult(new AreaEntity
                     {
                         AreaId = Guid.NewGuid(),
                         AreaName = "Fake area",
                         LiableEmployerId = Guid.NewGuid()
                     });
                 });
            var service = new ServiceCollection();
            service.AddTransient(_ => areaRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var areaSvc = provider.GetRequiredService<IAreaService>();
            var newArea = new AreaRequestDto
            {
                AreaName = "Fake area",
                LiableEmployerId = default,
            };
            var responseAdd = await areaSvc.AddArea(newArea).ConfigureAwait(false);
            Assert.NotNull(responseAdd);
            Assert.NotEqual(default, responseAdd);
        }
        [Fact]
        [UnitTest]
        public async Task Throw_Exception_When_Employed_dont_exist()
        {
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
                .Setup(e => e.SearchMatching(It.IsAny<Expression<Func<EmployedEntity, bool>>>()))
                .Returns(new List<EmployedEntity>());
            var service = new ServiceCollection();
            service.AddTransient(_ => employedRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var areaSvc = provider.GetRequiredService<IAreaService>();

            var newArea = new AreaRequestDto
            {
                AreaName = "Fake area",
                LiableEmployerId = Guid.Parse("31826538-6b06-4021-95c2-27fb184ac4fe")
            };
            var responseAdd = await Assert.ThrowsAsync<AreaEmployeIdDontExistException>(() =>
               areaSvc.AddArea(newArea)).ConfigureAwait(false);
            Assert.Equal($"El empleado con el id: {newArea.LiableEmployerId} no existe", responseAdd.Message);
        }
        [Fact]
        [UnitTest]
        public async Task Throw_Expection_when_EmployedId_already_exist_in_AreaEntity()
        {
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
                .Setup(e => e.SearchMatching(It.IsAny<Expression<Func<EmployedEntity, bool>>>()))
                .Returns(new List<EmployedEntity>{ new EmployedEntity {
                    EmployedId = Guid.NewGuid()
                }});
            
            var areaRepoMock = new Mock<IAreaRepository>();
            areaRepoMock
                .Setup(m => m.SearchMatching(It.IsAny<Expression<Func<AreaEntity, bool>>>()))
                .Returns(new List<AreaEntity> { new AreaEntity
                {
                    AreaId = Guid.NewGuid(),
                    AreaName = "Fake area",
                    LiableEmployerId =  Guid.Parse("31826538-6b06-4021-95c2-27fb184ac4fe")
                }});

            var service = new ServiceCollection();
            service.AddTransient(_ => areaRepoMock.Object);
            service.AddTransient(_ => employedRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var areaSvc = provider.GetRequiredService<IAreaService>();

            var newArea = new AreaRequestDto
            {
                AreaName = "Fake area",
                LiableEmployerId = Guid.Parse("31826538-6b06-4021-95c2-27fb184ac4fe")
            };
            var responseAdd = await Assert.ThrowsAsync<AreaLiableAlreadyExistException>(() =>
                areaSvc.AddArea(newArea)).ConfigureAwait(false);
            Assert.Equal($"El empleado con el id: {newArea.LiableEmployerId} ya esta asignado a una area", responseAdd.Message);
        }

        [Fact]
        [UnitTest]
        public async Task AddArea_Successfult_Test()
        {          
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
                .Setup(e => e.SearchMatching(It.IsAny<Expression<Func<EmployedEntity, bool>>>()))
                .Returns(new List<EmployedEntity>{ new EmployedEntity {
                    EmployedId = Guid.NewGuid()
                }});

            var areaRepoMock = new Mock<IAreaRepository>();
            areaRepoMock
                .Setup(m => m.SearchMatching(It.IsAny<Expression<Func<AreaEntity, bool>>>()))
                .Returns(new List<AreaEntity>());
            areaRepoMock
                 .Setup(x => x.Insert(It.IsAny<AreaEntity>()))
                 .Returns(() =>
                 {
                     return Task.FromResult(new AreaEntity 
                     { 
                         AreaId = Guid.NewGuid(),
                         AreaName = "Fake area",
                         LiableEmployerId = Guid.NewGuid()
                     });
                 });
            var service = new ServiceCollection();
            service.AddTransient(_ => areaRepoMock.Object);
            service.AddTransient(_ => employedRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var areaSvc = provider.GetRequiredService<IAreaService>();

            var newArea = new AreaRequestDto
            {
                AreaName = "Fake area",
                LiableEmployerId = Guid.NewGuid()
            };

            var responseAdd = await areaSvc.AddArea(newArea).ConfigureAwait(false);
            Assert.NotNull(responseAdd);
            Assert.NotEqual(default, responseAdd);
        }

        [Fact]
        [IntegrationTest]
        public async Task AddArea_Successfull_IntegrationTest()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings
            {
                ConnectionString = "Data Source=DESKTOP-A52QQCF\\SQLEXPRESS;Initial Catalog=BusinessAdministration;Integrated Security=True"
            });
            var provider = service.BuildServiceProvider();
            var areaSvc = provider.GetRequiredService<IAreaService>();

            var newArea = new AreaRequestDto
            {
                AreaName = "Fake area"
            };
            var responseAdd = await areaSvc.AddArea(newArea).ConfigureAwait(false);
            var newAreaDelete = new AreaDto
            {
                AreaId = Guid.Parse(responseAdd.ToString()),
                AreaName = "Fake area"
            };
            var responseDelete = areaSvc.DeleteArea(newAreaDelete);

            Assert.NotNull(responseAdd);
            Assert.NotEqual(default, responseAdd);
            Assert.True(responseDelete);
        }

    }
}
