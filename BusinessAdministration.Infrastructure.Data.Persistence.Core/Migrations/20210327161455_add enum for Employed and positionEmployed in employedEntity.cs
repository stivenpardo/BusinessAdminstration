using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessAdministration.Infrastructure.Data.Persistence.Core.Migrations
{
    public partial class addenumforEmployedandpositionEmployedinemployedEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeCode",
                table: "Employed");

            migrationBuilder.AlterColumn<int>(
                name: "EmployedPosition",
                table: "Employed",
                type: "int",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<int>(
                name: "EmployedCode",
                table: "Employed",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployedCode",
                table: "Employed");

            migrationBuilder.AlterColumn<string>(
                name: "EmployedPosition",
                table: "Employed",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeCode",
                table: "Employed",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
