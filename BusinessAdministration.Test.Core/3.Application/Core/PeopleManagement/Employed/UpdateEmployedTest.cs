using BusinessAdministration.Aplication.Core.PeopleManagement.Area.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Area;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;
using Xunit.Categories;

namespace BusinessAdministration.Test.Core._3.Application.Core.PeopleManagement.Employed
{
    public class UpdateEmployedTest
    {
        [Fact]
        [UnitTest]
        public void UpdateArea_Throw_Exception_when_AreaId_is_null_or_empty()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var areaSvc = provider.GetRequiredService<IAreaService>();

            Assert.Throws<AreaIdNotDefinedException>(() => areaSvc.UpdateArea(new AreaDto
            {
                AreaId = Guid.Empty,
                AreaName = "fake name",
                LiableEmployerId = Guid.NewGuid()
            }));
        }

        //[Fact]
        //[UnitTest]
        //public void UpdateArea_Successfult_Test()
        //{
        //    var areaRepoMock = new Mock<IAreaRepository>();
        //    areaRepoMock
        //         .Setup(x => x.Update(It.IsAny<AreaEntity>()))
        //         .Returns(() =>
        //         {
        //             return true;
        //         });
        //    var service = new ServiceCollection();
        //    service.AddTransient(_ => areaRepoMock.Object);
        //    service.ConfigurePeopleManagementService(new DbSettings());
        //    var provider = service.BuildServiceProvider();
        //    var areaSvc = provider.GetRequiredService<IAreaService>();

        //    var newArea = new AreaDto
        //    {
        //        AreaId = Guid.Parse("31826538-6b06-4021-95c2-27fb184ac4fe"),
        //        AreaName = "Fake area",
        //    };
        //    var response = areaSvc.UpdateArea(newArea);
        //    Assert.NotEqual(default, response);
        //    Assert.True(response);
        //}
    }
}
