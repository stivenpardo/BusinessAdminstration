﻿using BusinessAdministration.Aplication.Core.PeopleManagement.Configuration;
using BusinessAdministration.Aplication.Core.PeopleManagement.DocumentType.Services;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.DocumentType;
using BusinessAdministration.Aplication.Dto.PeopleManagement.DocumentType;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace BusinessAdministration.Test.Core._3.Application.Core.PeopleManagement.DocumentType
{
    public class AddDocumentTypeTest
    {
        [Fact]
        [UnitTest]
        public async Task AddDocumentType_Throw_Exception_when_DocumentType_are_null_or_empty()
        {
            var service = new ServiceCollection();
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var documentTypeSvc = provider.GetRequiredService<IDocumentTypeService>();

            await Assert.ThrowsAsync<DocumentTypeNotDefinedException>(() => documentTypeSvc.AddDocumentType(new DocumentTypeDto
            {
                DocumentType = string.Empty
            })).ConfigureAwait(false);
        }
        [Fact]
        [UnitTest]
        public async Task AddDocumentType_Successfult_Test()
        {
            var documentTypeRepoMock = new Mock<IDocumentTypeRepository>();
            documentTypeRepoMock
                 .Setup(x => x.Insert(It.IsAny<DocumentTypeEntity>()))
                 .Returns(() =>
                 {
                     return Task.FromResult(new DocumentTypeEntity
                     {
                         DocumentTypeId = Guid.NewGuid(),
                         DocumentType = "Fake area"
                     });
                 });
            var service = new ServiceCollection();
            service.AddTransient(_ => documentTypeRepoMock.Object);
            service.ConfigurePeopleManagementService(new DbSettings());
            var provider = service.BuildServiceProvider();
            var documentTypeSvc = provider.GetRequiredService<IDocumentTypeService>();

            var newDocumentType = new DocumentTypeDto
            {
                DocumentTypeId = Guid.NewGuid(),
                DocumentType = "Fake area",
            };

            var response = await documentTypeSvc.AddDocumentType(newDocumentType).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.NotEqual(default, response);
        }
        //[Fact]
        //[IntegrationTest]
        //public async Task AddArea_Successfull_IntegrationTest()
        //{
        //    //Todo: Creat Empleado, y elimnarlo al final para que no deje basura en la base de datos, igualmente para la entidad de area
        //    // tamnien llamar a los demas metodos como eliminar y actualizar
        //    var service = new ServiceCollection();
        //    service.ConfigurePeopleManagementService(new DbSettings
        //    {
        //        ConnectionString = "Data Source=DESKTOP-A52QQCF\\SQLEXPRESS;Initial Catalog=BusinessAdministration;Integrated Security=True"
        //    });
        //    var provider = service.BuildServiceProvider();
        //    var documentTypeSvc = provider.GetRequiredService<IAreaService>();

        //    var newDocumentType = new DocumentTypeDto
        //    {
        //        AreaName = "Fake area",
        //        ResponsableEmployedId = Guid.Parse("6b499387-b805-4339-8e8b-2d8bb08ba4eb")
        //    };
        //    var response = await documentTypeSvc.AddArea(newDocumentType).ConfigureAwait(false);

        //    Assert.NotNull(response);
        //    Assert.NotEqual(default, response);
        //}

    }
}
