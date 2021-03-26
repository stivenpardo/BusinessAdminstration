using BusinessAdministration.Aplication.Core.PeopleManagement.Area.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Area;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using BusinessAdministration.Domain.Core.PeopleManagement.Area;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace BusinessAdministration.Test.Core._3.Application.Core.PeopleManagement.Area
{
    public class GetAllAreaTest
    {
        [Fact]
        [UnitTest]
        public async Task Throw_exception_when_entity_Area_is_empty()
        {
            var areaRepoMock = new Mock<IAreaRepository>();
            areaRepoMock
                .Setup(m => m.GetAll<AreaEntity>())
                .Returns(new List<AreaEntity>());

            var service = new ServiceCollection();
            service.AddTransient(_ => areaRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var areaSvc = provider.GetRequiredService<IAreaService>();

            await Assert.ThrowsAsync<AreaEntityIsEmptyException>(() => areaSvc.GetAll()).ConfigureAwait(false);
        }

        [Fact]
        [UnitTest]
        public async Task Get_all_Successful()
        {
            var areaRepoMock = new Mock<IAreaRepository>();
            areaRepoMock
                .Setup(m => m.GetAll<AreaEntity>())
                .Returns(new List<AreaEntity> { new AreaEntity
                {
                    AreaId= Guid.NewGuid(),
                    AreaName = "fake name",
                    ResponsableEmployedId = Guid.NewGuid()
                },
                 new AreaEntity
                {
                    AreaId= Guid.NewGuid(),
                    AreaName = "fake name2",
                    ResponsableEmployedId = Guid.NewGuid()
                }});

            var service = new ServiceCollection();
            service.AddTransient(_ => areaRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var areaSvc = provider.GetRequiredService<IAreaService>();

            var response =  await areaSvc.GetAll().ConfigureAwait(false);
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
