using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessAdministration.Infrastructure.Data.Persistence.Core.Migrations
{
    public partial class Refactornamespropetiesofentitysasperson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Provider",
                newName: "PersonPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Provider",
                newName: "PersonName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Provider",
                newName: "PersonLastName");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Provider",
                newName: "PersonDateOfBirth");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Employed",
                newName: "PersonPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Employed",
                newName: "PersonName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Employed",
                newName: "PersonLastName");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Employed",
                newName: "PersonDateOfBirth");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Customer",
                newName: "PersonPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Customer",
                newName: "PersonName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Customer",
                newName: "PersonLastName");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Customer",
                newName: "PersonDateOfBirth");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonPhoneNumber",
                table: "Provider",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "PersonName",
                table: "Provider",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PersonLastName",
                table: "Provider",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "PersonDateOfBirth",
                table: "Provider",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "PersonPhoneNumber",
                table: "Employed",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "PersonName",
                table: "Employed",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PersonLastName",
                table: "Employed",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "PersonDateOfBirth",
                table: "Employed",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "PersonPhoneNumber",
                table: "Customer",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "PersonName",
                table: "Customer",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PersonLastName",
                table: "Customer",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "PersonDateOfBirth",
                table: "Customer",
                newName: "DateOfBirth");
        }
    }
}
