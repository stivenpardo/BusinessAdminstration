using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessAdministration.Infrastructure.Data.Persistence.Core.Migrations
{
    public partial class RefactornamespropetiesofentitysasEmployedandpeson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "creationDate",
                table: "Provider",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "creationDate",
                table: "Employed",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "creationDate",
                table: "Customer",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "ResponsableEmployedId",
                table: "Area",
                newName: "LiableEmployerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Provider",
                newName: "creationDate");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Employed",
                newName: "creationDate");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Customer",
                newName: "creationDate");

            migrationBuilder.RenameColumn(
                name: "LiableEmployerId",
                table: "Area",
                newName: "ResponsableEmployedId");
        }
    }
}
