using Microsoft.EntityFrameworkCore.Migrations;

namespace Pardisan.Migrations
{
    public partial class addPropIntoTheEstateFloorAndUnits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FloorName",
                table: "Floors",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Meterage",
                table: "EstateUnits",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "EstateUnits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FloorName",
                table: "Floors");

            migrationBuilder.DropColumn(
                name: "Meterage",
                table: "EstateUnits");

            migrationBuilder.DropColumn(
                name: "name",
                table: "EstateUnits");
        }
    }
}
