﻿// <auto-generated />
using System;
using BusinessAdministration.Infrastructure.Data.Persistence.Core.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BusinessAdministration.Infrastructure.Data.Persistence.Core.Migrations
{
    [DbContext(typeof(ContextDb))]
    [Migration("20210325060813_Refactor employed")]
    partial class Refactoremployed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BusinessAdministration.Domain.Core.PeopleManagement.Area.AreaEntity", b =>
                {
                    b.Property<Guid>("AreaId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(30)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AreaName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<Guid>("ResponsableEmployedId")
                        .HasMaxLength(30)
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AreaId");

                    b.ToTable("Area");
                });

            modelBuilder.Entity("BusinessAdministration.Domain.Core.PeopleManagement.Customer.CustomerEntity", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(30)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateOfBirth")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("DocumentEntityDocumentTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DocumentTypeId")
                        .HasMaxLength(30)
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("IdentificationNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PersonEmail")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("PersonType")
                        .HasColumnType("int");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("creationDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("CustomerId");

                    b.HasIndex("DocumentEntityDocumentTypeId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("BusinessAdministration.Domain.Core.PeopleManagement.DocumentType.DocumentTypeEntity", b =>
                {
                    b.Property<Guid>("DocumentTypeId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(30)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("DocumentTypeId");

                    b.ToTable("DocumentType");
                });

            modelBuilder.Entity("BusinessAdministration.Domain.Core.PeopleManagement.Employed.EmployedEntity", b =>
                {
                    b.Property<Guid>("EmployedId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(30)
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AreaId")
                        .HasMaxLength(30)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateOfBirth")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("DocumentTypeId")
                        .HasMaxLength(30)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmployedPosition")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<Guid>("EmployeeCode")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("IdentificationNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PersonEmail")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("PersonType")
                        .HasColumnType("int");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("creationDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("EmployedId");

                    b.HasIndex("AreaId");

                    b.HasIndex("DocumentTypeId");

                    b.ToTable("Employed");
                });

            modelBuilder.Entity("BusinessAdministration.Domain.Core.PeopleManagement.Provider.ProviderEntity", b =>
                {
                    b.Property<Guid>("ProviderId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(30)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateOfBirth")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("DocumentEntityDocumentTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DocumentTypeId")
                        .HasMaxLength(30)
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("IdentificationNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PersonBusinessName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PersonEmail")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("PersonType")
                        .HasColumnType("int");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("creationDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("ProviderId");

                    b.HasIndex("DocumentEntityDocumentTypeId");

                    b.ToTable("Provider");
                });

            modelBuilder.Entity("BusinessAdministration.Domain.Core.PeopleManagement.Customer.CustomerEntity", b =>
                {
                    b.HasOne("BusinessAdministration.Domain.Core.PeopleManagement.DocumentType.DocumentTypeEntity", "DocumentEntity")
                        .WithMany("CustomerList")
                        .HasForeignKey("DocumentEntityDocumentTypeId");

                    b.Navigation("DocumentEntity");
                });

            modelBuilder.Entity("BusinessAdministration.Domain.Core.PeopleManagement.Employed.EmployedEntity", b =>
                {
                    b.HasOne("BusinessAdministration.Domain.Core.PeopleManagement.Area.AreaEntity", "Area")
                        .WithMany("EmployeesList")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessAdministration.Domain.Core.PeopleManagement.DocumentType.DocumentTypeEntity", "DocumentType")
                        .WithMany("EmployeesList")
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("DocumentType");
                });

            modelBuilder.Entity("BusinessAdministration.Domain.Core.PeopleManagement.Provider.ProviderEntity", b =>
                {
                    b.HasOne("BusinessAdministration.Domain.Core.PeopleManagement.DocumentType.DocumentTypeEntity", "DocumentEntity")
                        .WithMany("ProvidersList")
                        .HasForeignKey("DocumentEntityDocumentTypeId");

                    b.Navigation("DocumentEntity");
                });

            modelBuilder.Entity("BusinessAdministration.Domain.Core.PeopleManagement.Area.AreaEntity", b =>
                {
                    b.Navigation("EmployeesList");
                });

            modelBuilder.Entity("BusinessAdministration.Domain.Core.PeopleManagement.DocumentType.DocumentTypeEntity", b =>
                {
                    b.Navigation("CustomerList");

                    b.Navigation("EmployeesList");

                    b.Navigation("ProvidersList");
                });
#pragma warning restore 612, 618
        }
    }
}
