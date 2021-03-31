using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.Employed.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
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
    public class DeleteEmployedTest
    {
        [Fact]
        [UnitTest]
        public void DeleteEmployed_Throw_IdCannotNullOrEmptyException_when_EmployedId_is_null_or_empty()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            Assert.Throws<IdCannotNullOrEmptyException>(() => employedSvc.DeleteEmployed(new EmployedRequestDto
            {
                EmployedId = Guid.Empty
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

            var newEmployed = new EmployedRequestDto
            {
                EmployedId = Guid.NewGuid(),
            };
            Assert.Throws<DontExistIdException>(() => employedSvc.DeleteEmployed(newEmployed));
        }
        [Fact]
        [UnitTest]
        public void DeleteEmployed_Successfult_Test()
        {
            var employedRepoMock = new Mock<IEmployedRepository>();
            employedRepoMock
                .Setup(e => e.SearchMatching(It.IsAny<Expression<Func<EmployedEntity, bool>>>()))
                .Returns(new List<EmployedEntity> { new EmployedEntity
                {
                    EmployedId = Guid.NewGuid()
                }});

            employedRepoMock
                .Setup(e => e.Delete(It.IsAny<EmployedEntity>()))
                .Returns( ()=> 
                {
                    return true;
                });

            var service = new ServiceCollection();
            service.AddTransient(_ => employedRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var employedSvc = provider.GetRequiredService<IEmployedService>();

            var newEmployed = new EmployedRequestDto
            {
                EmployedId = Guid.Parse("31826538-6b06-4021-95c2-27fb184ac4fe")
            };

            var responseDelete = employedSvc.DeleteEmployed(newEmployed);
            Assert.NotEqual(default, responseDelete);
            Assert.True(responseDelete);
        }
    }
}
