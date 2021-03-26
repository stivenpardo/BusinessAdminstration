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
                ResponsableEmployedId = Guid.NewGuid()
            })).ConfigureAwait(false);

            await Assert.ThrowsAsync<AreaNameNotDefinedException>(() => areaSvc.AddArea(new AreaRequestDto
            {
                AreaName = null,
                ResponsableEmployedId = Guid.NewGuid()
            })).ConfigureAwait(false);

            await Assert.ThrowsAsync<AreaLiableEmployeedIdNotDefinedException>(() => areaSvc.AddArea(new AreaRequestDto
            {
                AreaName = "Fake area",
                ResponsableEmployedId = Guid.Empty
            })).ConfigureAwait(false);

            await Assert.ThrowsAsync<AreaLiableEmployeedIdNotDefinedException>(() => areaSvc.AddArea(new AreaRequestDto
            {
                AreaName = "Fake area",
                ResponsableEmployedId = default
            })).ConfigureAwait(false);
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
                ResponsableEmployedId = Guid.Parse("31826538-6b06-4021-95c2-27fb184ac4fe")
            };
            var response = await Assert.ThrowsAsync<AreaEmployeIdDontExistException>(() =>
               areaSvc.AddArea(newArea)).ConfigureAwait(false);
            Assert.Equal(newArea.ResponsableEmployedId.ToString(), response.Message);
        }
        [Fact]
        [UnitTest]
        public async Task Throws_Expection_when_EmployedId_already_exist_in_AreaEntity()
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
                    ResponsableEmployedId =  Guid.Parse("31826538-6b06-4021-95c2-27fb184ac4fe")
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
                ResponsableEmployedId = Guid.Parse("31826538-6b06-4021-95c2-27fb184ac4fe")
            };
            var response = await Assert.ThrowsAsync<AreaLiableAlreadyExistException>(() =>
                areaSvc.AddArea(newArea)).ConfigureAwait(false);
            Assert.Equal(newArea.ResponsableEmployedId.ToString(), response.Message);
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
                         ResponsableEmployedId = Guid.NewGuid()
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
                ResponsableEmployedId = Guid.NewGuid()
            };

            var response = await areaSvc.AddArea(newArea).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.NotEqual(default, response);
        }
        [Fact]
        [IntegrationTest]
        public async Task AddArea_Successfull_IntegrationTest()
        {
            //Todo: Creat Empleado, y elimnarlo al final para que no deje basura en la base de datos, igualmente para la entidad de area
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings
            {
                ConnectionString = "Data Source=DESKTOP-A52QQCF\\SQLEXPRESS;Initial Catalog=BusinessAdministration;Integrated Security=True"
            });
            var provider = service.BuildServiceProvider();
            var areaSvc = provider.GetRequiredService<IAreaService>();

            var newArea = new AreaRequestDto
            {
                AreaName = "Fake area",
                ResponsableEmployedId = Guid.Parse("6b499387-b805-4339-8e8b-2d8bb08ba4eb")
            };
            var response = await areaSvc.AddArea(newArea).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.NotEqual(default, response);
        }

    }
}
