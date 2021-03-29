using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.DocumentType;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace BusinessAdministration.Test.Core._3.Application.Core.PeopleManagement.DocumentType
{
    public class GetAllDocumentTypeTest
    {
        [Fact]
        [UnitTest]
        public async Task Throw_exception_when_entity_DocumentType_is_empty()
        {
            var documentTypeRepoMock = new Mock<IDocumentTypeRepository>();
            documentTypeRepoMock
                .Setup(m => m.GetAll<DocumentTypeEntity>())
                .Returns(new List<DocumentTypeEntity>());

            var service = new ServiceCollection();
            service.AddTransient(_ => documentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var documentTypeSvc = provider.GetRequiredService<IDocumentTypeService>();

            await Assert.ThrowsAsync<DocumentTypeEntityIsEmptyException>(() => documentTypeSvc.GetAll()).ConfigureAwait(false);
        }

        [Fact]
        [UnitTest]
        public async Task Get_all_Successful()
        {
            var documentTypeRepoMock = new Mock<IDocumentTypeRepository>();
            documentTypeRepoMock
                .Setup(m => m.GetAll<DocumentTypeEntity>())
                .Returns(new List<DocumentTypeEntity> { new DocumentTypeEntity
                {
                   DocumentTypeId = Guid.NewGuid(),
                   DocumentType = "Fake area2"
                },
                 new DocumentTypeEntity
                {
                   DocumentTypeId = Guid.NewGuid(),
                   DocumentType = "Fake area"
                }});

            var service = new ServiceCollection();
            service.AddTransient(_ => documentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var documentTypeSvc = provider.GetRequiredService<IDocumentTypeService>();

            var response = await documentTypeSvc.GetAll().ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.NotEqual(default, response);
        }
        [Fact]
        [IntegrationTest]
        public async Task GetAllDocumentType_Successfull_IntegrationTest()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings
            {
                ConnectionString = "Data Source=DESKTOP-A52QQCF\\SQLEXPRESS;Initial Catalog=BusinessAdministration;Integrated Security=True"
            });
            var provider = service.BuildServiceProvider();
            var documentTypeSvc = provider.GetRequiredService<IDocumentTypeService>();

            var responseSearch = await documentTypeSvc.GetAll().ConfigureAwait(false);

            Assert.NotNull(responseSearch);
            Assert.NotEqual(default, responseSearch);
        }

    }
}
