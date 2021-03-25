using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessAdministration.Infrastructure.Data.Persistence.Core.Migrations
{
    public partial class RefactorforenKeyofProvideryCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_DocumentType_DocumentEntityDocumentTypeId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Provider_DocumentType_DocumentEntityDocumentTypeId",
                table: "Provider");

            migrationBuilder.DropIndex(
                name: "IX_Provider_DocumentEntityDocumentTypeId",
                table: "Provider");

            migrationBuilder.DropIndex(
                name: "IX_Customer_DocumentEntityDocumentTypeId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "DocumentEntityDocumentTypeId",
                table: "Provider");

            migrationBuilder.DropColumn(
                name: "DocumentEntityDocumentTypeId",
                table: "Customer");

            migrationBuilder.CreateIndex(
                name: "IX_Provider_DocumentTypeId",
                table: "Provider",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DocumentTypeId",
                table: "Customer",
                column: "DocumentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_DocumentType_DocumentTypeId",
                table: "Customer",
                column: "DocumentTypeId",
                principalTable: "DocumentType",
                principalColumn: "DocumentTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Provider_DocumentType_DocumentTypeId",
                table: "Provider",
                column: "DocumentTypeId",
                principalTable: "DocumentType",
                principalColumn: "DocumentTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_DocumentType_DocumentTypeId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Provider_DocumentType_DocumentTypeId",
                table: "Provider");

            migrationBuilder.DropIndex(
                name: "IX_Provider_DocumentTypeId",
                table: "Provider");

            migrationBuilder.DropIndex(
                name: "IX_Customer_DocumentTypeId",
                table: "Customer");

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentEntityDocumentTypeId",
                table: "Provider",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentEntityDocumentTypeId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Provider_DocumentEntityDocumentTypeId",
                table: "Provider",
                column: "DocumentEntityDocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DocumentEntityDocumentTypeId",
                table: "Customer",
                column: "DocumentEntityDocumentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_DocumentType_DocumentEntityDocumentTypeId",
                table: "Customer",
                column: "DocumentEntityDocumentTypeId",
                principalTable: "DocumentType",
                principalColumn: "DocumentTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Provider_DocumentType_DocumentEntityDocumentTypeId",
                table: "Provider",
                column: "DocumentEntityDocumentTypeId",
                principalTable: "DocumentType",
                principalColumn: "DocumentTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
