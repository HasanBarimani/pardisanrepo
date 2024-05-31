using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pardisan.Migrations
{
    public partial class modificationInEstateTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AparatLink",
                table: "Estates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstateMeterage",
                table: "Estates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProjectCompletionDate",
                table: "Estates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TotalUnits",
                table: "Estates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitInFlorCount",
                table: "Estates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsageType",
                table: "Estates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AparatLink",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "EstateMeterage",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "ProjectCompletionDate",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "TotalUnits",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "UnitInFlorCount",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "UsageType",
                table: "Estates");
        }
    }
}
