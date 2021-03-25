using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessAdministration.Infrastructure.Data.Persistence.Core.Migrations
{
    public partial class Refactoremployed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employed_DocumentType_DocumentEntityDocumentTypeId",
                table: "Employed");

            migrationBuilder.DropIndex(
                name: "IX_Employed_DocumentEntityDocumentTypeId",
                table: "Employed");

            migrationBuilder.DropColumn(
                name: "DocumentEntityDocumentTypeId",
                table: "Employed");

            migrationBuilder.CreateIndex(
                name: "IX_Employed_DocumentTypeId",
                table: "Employed",
                column: "DocumentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employed_DocumentType_DocumentTypeId",
                table: "Employed",
                column: "DocumentTypeId",
                principalTable: "DocumentType",
                principalColumn: "DocumentTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employed_DocumentType_DocumentTypeId",
                table: "Employed");

            migrationBuilder.DropIndex(
                name: "IX_Employed_DocumentTypeId",
                table: "Employed");

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentEntityDocumentTypeId",
                table: "Employed",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employed_DocumentEntityDocumentTypeId",
                table: "Employed",
                column: "DocumentEntityDocumentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employed_DocumentType_DocumentEntityDocumentTypeId",
                table: "Employed",
                column: "DocumentEntityDocumentTypeId",
                principalTable: "DocumentType",
                principalColumn: "DocumentTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
