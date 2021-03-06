using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessAdministration.Infrastructure.Data.Persistence.Core.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    AreaId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 30, nullable: false),
                    AreaName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LiableEmployerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.AreaId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    DocumentTypeId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 30, nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.DocumentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 30, nullable: false),
                    DocumentTypeId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 30, nullable: false),
                    IdentificationNumber = table.Column<long>(type: "bigint", nullable: false),
                    PersonType = table.Column<int>(type: "int", nullable: false),
                    PersonName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PersonLastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PersonDateOfBirth = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PersonPhoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    PersonEmail = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customer_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentType",
                        principalColumn: "DocumentTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employed",
                columns: table => new
                {
                    EmployedId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 30, nullable: false),
                    EmployedCode = table.Column<Guid>(type: "uniqueidentifier", maxLength: 30, nullable: false),
                    PersonType = table.Column<int>(type: "int", nullable: false),
                    EmployedPosition = table.Column<int>(type: "int", nullable: false),
                    AreaId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 30, nullable: false),
                    DocumentTypeId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 30, nullable: false),
                    IdentificationNumber = table.Column<long>(type: "bigint", nullable: false),
                    PersonName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PersonLastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PersonDateOfBirth = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PersonPhoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    PersonEmail = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employed", x => x.EmployedId);
                    table.ForeignKey(
                        name: "FK_Employed_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "AreaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employed_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentType",
                        principalColumn: "DocumentTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Provider",
                columns: table => new
                {
                    ProviderId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 30, nullable: false),
                    PersonBusinessName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DocumentTypeId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 30, nullable: false),
                    IdentificationNumber = table.Column<long>(type: "bigint", nullable: false),
                    PersonType = table.Column<int>(type: "int", nullable: false),
                    PersonName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PersonLastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PersonDateOfBirth = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PersonPhoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    PersonEmail = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provider", x => x.ProviderId);
                    table.ForeignKey(
                        name: "FK_Provider_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentType",
                        principalColumn: "DocumentTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DocumentTypeId",
                table: "Customer",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employed_AreaId",
                table: "Employed",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Employed_DocumentTypeId",
                table: "Employed",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Provider_DocumentTypeId",
                table: "Provider",
                column: "DocumentTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Employed");

            migrationBuilder.DropTable(
                name: "Provider");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "DocumentType");
        }
    }
}
