using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.DocumentType;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace BusinessAdministration.Test.Core._3.Application.Core.PeopleManagement.DocumentType
{
    public class GetByIdDocumentTypeTest
    {
        [Fact]
        [UnitTest]
        public async Task Throw_exception_when_Id_is_empty_or_null()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var documentTypeSvc = provider.GetRequiredService<IDocumentTypeService>();

            var id = Guid.Empty;

            await Assert.ThrowsAsync<DontExistIdException>(() => documentTypeSvc.GetById(id)).ConfigureAwait(false);
        }

        [Fact]
        [UnitTest]
        public async Task Throw_Exception_NoExistDocumentTypeException_When_Dont_exist_Register_Test()
        {
            var documentTypeRepoMock = new Mock<IDocumentTypeRepository>();
            documentTypeRepoMock
                .Setup(m => m.SearchMatchingOneResult(It.IsAny<Expression<Func<DocumentTypeEntity, bool>>>()));

            var service = new ServiceCollection();
            service.AddTransient(_ => documentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var documentTypeSvc = provider.GetRequiredService<IDocumentTypeService>();

            var id = Guid.NewGuid();
            var response = await Assert.ThrowsAsync<NoExistDocumentTypeException>(() =>
                documentTypeSvc.GetById(id)).ConfigureAwait(false);
        }


        [Fact]
        [UnitTest]
        public async Task GetById_Successful_Test()
        {
            var documentTypeRepoMock = new Mock<IDocumentTypeRepository>();
            documentTypeRepoMock
                .Setup(m => m.SearchMatchingOneResult(It.IsAny<Expression<Func<DocumentTypeEntity, bool>>>()))
                .Returns(new DocumentTypeEntity());

            var service = new ServiceCollection();
            service.AddTransient(_ => documentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var documentTypeSvc = provider.GetRequiredService<IDocumentTypeService>();

            var id = Guid.NewGuid();
            var response = await documentTypeSvc.GetById(id).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.NotEqual(default, response);
        }

        [Fact]
        [IntegrationTest]
        public async Task GetAllByID_Successfull_IntegrationTest()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings
            {
                ConnectionString = "Data Source=DESKTOP-A52QQCF\\SQLEXPRESS;Initial Catalog=BusinessAdministration;Integrated Security=True"
            });
            var provider = service.BuildServiceProvider();
            var documentTypeSvc = provider.GetRequiredService<IDocumentTypeService>();

            var id = Guid.Parse("06DC9461-2602-43A4-1058-08D910D10553");

            var responseSearch = await documentTypeSvc.GetById(id).ConfigureAwait(false);

            Assert.NotNull(responseSearch);
            Assert.NotEqual(default, responseSearch);
        }

    }
}
