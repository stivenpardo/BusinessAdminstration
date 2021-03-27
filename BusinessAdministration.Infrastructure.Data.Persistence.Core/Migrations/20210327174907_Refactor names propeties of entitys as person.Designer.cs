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
    [Migration("20210327174907_Refactor names propeties of entitys as person")]
    partial class Refactornamespropetiesofentitysasperson
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

                    b.Property<Guid>("LiableEmployerId")
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

                    b.Property<DateTimeOffset>("CreationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("DocumentTypeId")
                        .HasMaxLength(30)
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("IdentificationNumber")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("PersonDateOfBirth")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("PersonEmail")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PersonLastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PersonName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<long>("PersonPhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("PersonType")
                        .HasColumnType("int");

                    b.HasKey("CustomerId");

                    b.HasIndex("DocumentTypeId");

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

                    b.Property<DateTimeOffset>("CreationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("DocumentTypeId")
                        .HasMaxLength(30)
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("EmployedCode")
                        .HasColumnType("int");

                    b.Property<int>("EmployedPosition")
                        .HasColumnType("int");

                    b.Property<long>("IdentificationNumber")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("PersonDateOfBirth")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("PersonEmail")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PersonLastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PersonName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<long>("PersonPhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("PersonType")
                        .HasColumnType("int");

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

                    b.Property<DateTimeOffset>("CreationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("DocumentTypeId")
                        .HasMaxLength(30)
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("IdentificationNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("PersonBusinessName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTimeOffset>("PersonDateOfBirth")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("PersonEmail")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PersonLastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PersonName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<long>("PersonPhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("PersonType")
                        .HasColumnType("int");

                    b.HasKey("ProviderId");

                    b.HasIndex("DocumentTypeId");

                    b.ToTable("Provider");
                });

            modelBuilder.Entity("BusinessAdministration.Domain.Core.PeopleManagement.Customer.CustomerEntity", b =>
                {
                    b.HasOne("BusinessAdministration.Domain.Core.PeopleManagement.DocumentType.DocumentTypeEntity", "DocumentType")
                        .WithMany("CustomerList")
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentType");
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
                    b.HasOne("BusinessAdministration.Domain.Core.PeopleManagement.DocumentType.DocumentTypeEntity", "DocumentType")
                        .WithMany("ProvidersList")
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentType");
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
