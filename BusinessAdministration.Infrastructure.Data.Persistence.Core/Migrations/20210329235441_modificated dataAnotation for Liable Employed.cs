using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessAdministration.Infrastructure.Data.Persistence.Core.Migrations
{
    public partial class modificateddataAnotationforLiableEmployed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "LiableEmployerId",
                table: "Area",
                type: "uniqueidentifier",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 30);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "LiableEmployerId",
                table: "Area",
                type: "uniqueidentifier",
                maxLength: 30,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 30,
                oldNullable: true);
        }
    }
}
