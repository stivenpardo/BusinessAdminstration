using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.DocumentType;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person;
using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
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

namespace BusinessAdministration.Test.Core._3.Application.Core.PeopleManagement.DocumentType
{
    public class UpdateDocumentTypeTest
    {
        [Fact]
        [UnitTest]
        public void UpdateDocumentType_Throw_Exception_when_DocumentId_is_null_or_empty()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var documentTypeSvc = provider.GetRequiredService<IDocumentTypeService>();

            Assert.Throws<DocumentTypeIdNotDefinedException>(() => documentTypeSvc.UpdateDocumentType(new DocumentTypeDto
            {
                DocumentTypeId = Guid.Empty,
                DocumentType = "Fake area"
            }));
        }
        [Fact]
        [UnitTest]
        public void Throw_DontExistIdException_when_id_it_isnt()
        {
            var documentTypeRepoMock = new Mock<IDocumentTypeRepository>();
            documentTypeRepoMock
                 .Setup(x => x.SearchMatching(It.IsAny<Expression<Func<DocumentTypeEntity, bool>>>()))
                 .Returns(new List<DocumentTypeEntity>());
            var service = new ServiceCollection();
            service.AddTransient(_ => documentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var documentTypeSvc = provider.GetRequiredService<IDocumentTypeService>();

            var newDocumentType = new DocumentTypeDto
            {
                DocumentTypeId = Guid.NewGuid(),
                DocumentType = "Fake area"
            };
            Assert.Throws<DontExistIdException>(() => documentTypeSvc.UpdateDocumentType(newDocumentType));
        }

        [Fact]
        [UnitTest]
        public void UpdateDocumentType_Successfult_Test()
        {
            var documentTypeRepoMock = new Mock<IDocumentTypeRepository>();
            documentTypeRepoMock
               .Setup(x => x.SearchMatching(It.IsAny<Expression<Func<DocumentTypeEntity, bool>>>()))
               .Returns(new List<DocumentTypeEntity> { new DocumentTypeEntity 
               {
                   DocumentTypeId= Guid.NewGuid()
               }});
            documentTypeRepoMock
                 .Setup(x => x.Update(It.IsAny<DocumentTypeEntity>()))
                 .Returns(() =>
                 {
                     return true;
                 });
            var service = new ServiceCollection();
            service.AddTransient(_ => documentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var documentTypeSvc = provider.GetRequiredService<IDocumentTypeService>();

            var newArea = new DocumentTypeDto
            {
                DocumentTypeId = Guid.NewGuid(),
                DocumentType = "Fake area"
            };
            var response = documentTypeSvc.UpdateDocumentType(newArea);
            Assert.NotEqual(default, response);
            Assert.True(response);
        }
        [Fact]
        [IntegrationTest]
        public async Task UpdateDocumentType_Successfull_IntegrationTest()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings
            {
                ConnectionString = "Data Source=DESKTOP-A52QQCF\\SQLEXPRESS;Initial Catalog=BusinessAdministration;Integrated Security=True"
            });
            var provider = service.BuildServiceProvider();
            var documentTypeSvc = provider.GetRequiredService<IDocumentTypeService>();

            var responseSearch = await documentTypeSvc.GetAll().ConfigureAwait(false);
            var documentType = responseSearch.FirstOrDefault();
            var newDocumentType = new DocumentTypeDto
            {
                DocumentTypeId = documentType.DocumentTypeId,
                DocumentType = "Registro",
            };
            var responseUpdate = documentTypeSvc.UpdateDocumentType(newDocumentType);
            Assert.NotNull(responseSearch);
            Assert.NotEqual(default, responseSearch);
            Assert.True(responseUpdate);
        }
    }
}
