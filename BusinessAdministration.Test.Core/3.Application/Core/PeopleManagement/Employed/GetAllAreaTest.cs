using BusinessAdministration.Aplication.Core.PeopleManagement.Area.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Area;
using BusinessAdministration.Domain.Core.PeopleManagement.Area;
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
                    LiableEmployerId = Guid.NewGuid()
                },
                 new AreaEntity
                {
                    AreaId= Guid.NewGuid(),
                    AreaName = "fake name2",
                    LiableEmployerId = Guid.NewGuid()
                }});

            var service = new ServiceCollection();
            service.AddTransient(_ => areaRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var areaSvc = provider.GetRequiredService<IAreaService>();

            var response = await areaSvc.GetAll().ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.NotEqual(default, response);
        }

    }
}
