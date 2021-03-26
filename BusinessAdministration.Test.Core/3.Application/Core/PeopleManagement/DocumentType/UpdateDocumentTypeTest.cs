using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.DocumentType;
using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
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
        public void UpdateDocumentType_Successfult_Test()
        {
            var documentTypeRepoMock = new Mock<IDocumentTypeRepository>();
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
    }
}
