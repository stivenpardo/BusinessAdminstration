using BusinessAdministration.Aplication.Core.PeopleManagement.Area.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Area;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using BusinessAdministration.Domain.Core.PeopleManagement.Area;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace BusinessAdministration.Test.Core._3.Application.Core.PeopleManagement.Area
{
    public class UpdateAreaTest
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
        [Fact]
        [UnitTest]
        public void Throw_DontExistIdException_when_id_it_isnt()
        {
            var areaRepoMock = new Mock<IAreaRepository>();
            areaRepoMock
                 .Setup(x => x.SearchMatching(It.IsAny<Expression<Func<AreaEntity, bool>>>()))
                 .Returns(new List<AreaEntity>());
            var service = new ServiceCollection();
            service.AddTransient(_ => areaRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var areaSvc = provider.GetRequiredService<IAreaService>();

            var newDocumentType = new AreaDto
            {
                AreaId = Guid.NewGuid()
            };
            Assert.Throws<DontExistIdException>(() => areaSvc.UpdateArea(newDocumentType));
        }

        [Fact]
        [UnitTest]
        public void UpdateArea_Successfult_Test()
        {
            var areaRepoMock = new Mock<IAreaRepository>();
            areaRepoMock
                 .Setup(x => x.SearchMatching(It.IsAny<Expression<Func<AreaEntity, bool>>>()))
                 .Returns(new List<AreaEntity> { new AreaEntity
                 {
                     AreaId= Guid.NewGuid()
                 }});
            areaRepoMock
                 .Setup(x => x.Update(It.IsAny<AreaEntity>()))
                 .Returns(() =>
                 {
                     return true;
                 });
            var service = new ServiceCollection();
            service.AddTransient(_ => areaRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var areaSvc = provider.GetRequiredService<IAreaService>();

            var newArea = new AreaDto
            {
                AreaId = Guid.Parse("31826538-6b06-4021-95c2-27fb184ac4fe"),
                AreaName = "Fake area",
            };
            var response = areaSvc.UpdateArea(newArea);
            Assert.NotEqual(default, response);
            Assert.True(response);
        }
        [Fact]
        [IntegrationTest]
        public async Task UpdateArea_Successfull_IntegrationTest()
        {
            //TODO: Michael have to creato employed for create area. 
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings
            {
                ConnectionString = "Data Source=DESKTOP-A52QQCF\\SQLEXPRESS;Initial Catalog=BusinessAdministration;Integrated Security=True"
            });
            var provider = service.BuildServiceProvider();
            var areaSvc = provider.GetRequiredService<IAreaService>();

            var responseSearch = await areaSvc.GetAll().ConfigureAwait(false);
            var responseUpdate = areaSvc.UpdateArea(responseSearch.FirstOrDefault());

            Assert.NotEqual(default, responseSearch);
            Assert.NotNull(responseSearch);
            Assert.True(responseUpdate);
        }
    }
}
