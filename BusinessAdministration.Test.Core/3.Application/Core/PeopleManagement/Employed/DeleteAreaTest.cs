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
using Xunit;
using Xunit.Categories;

namespace BusinessAdministration.Test.Core._3.Application.Core.PeopleManagement.Employed
{
    public class DeleteAreaTest
    {
        [Fact]
        [UnitTest]
        public void DeleteArea_Throw_Exception_when_AreaId_is_null_or_empty()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var areaSvc = provider.GetRequiredService<IAreaService>();

            Assert.Throws<AreaIdNotDefinedException>(() => areaSvc.DeleteArea(new AreaDto
            {
                AreaId = Guid.Empty,
                AreaName = "fake name",
                LiableEmployerId = Guid.NewGuid()
            }));
        }
        [Fact]
        [UnitTest]
        public void Throw_Exception_When_AreaId_is_associate_to_employed()
        {
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
                .Setup(e => e.SearchMatching(It.IsAny<Expression<Func<EmployedEntity, bool>>>()))
                .Returns(new List<EmployedEntity> { new EmployedEntity {
                    EmployedId = Guid.NewGuid(),
                    PersonName = "fakeName",
                    AreaId = Guid.Parse("31826538-6b06-4021-95c2-27fb184ac4fe")
                } });
            var service = new ServiceCollection();
            service.AddTransient(_ => employedRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var areaSvc = provider.GetRequiredService<IAreaService>();

            var newArea = new AreaDto
            {
                AreaId = Guid.Parse("31826538-6b06-4021-95c2-27fb184ac4fe"),
                AreaName = "Fake area",
                LiableEmployerId = Guid.Parse("31826538-6b06-4021-95c2-27fb184ac4de")
            };
            var response = Assert.Throws<AreaIdIsAssociatedToEmployedException>(() =>
               areaSvc.DeleteArea(newArea));
            Assert.Equal(newArea.AreaId.ToString(), response.Message);
        }
        [Fact]
        [UnitTest]
        public void DeleteArea_Successfult_Test()
        {
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
                .Setup(e => e.SearchMatching(It.IsAny<Expression<Func<EmployedEntity, bool>>>()))
                .Returns(new List<EmployedEntity>());

            var areaRepoMock = new Mock<IAreaRepository>();
            areaRepoMock
                 .Setup(x => x.Delete(It.IsAny<AreaEntity>()))
                 .Returns(() =>
                 {
                     return true;
                 });
            var service = new ServiceCollection();
            service.AddTransient(_ => employedRepoMock.Object);
            service.AddTransient(_ => areaRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var areaSvc = provider.GetRequiredService<IAreaService>();

            var newArea = new AreaDto
            {
                AreaId = Guid.Parse("31826538-6b06-4021-95c2-27fb184ac4fe")
            };

            var response = areaSvc.DeleteArea(newArea);
            Assert.NotEqual(default, response);
            Assert.True(response);
        }

    }
}
