using Microsoft.EntityFrameworkCore.Migrations;

namespace Pardisan.Migrations
{
    public partial class OptionsInEstate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AbNama",
                table: "Estates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Alachigh",
                table: "Estates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Camera",
                table: "Estates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Fibr",
                table: "Estates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Flower",
                table: "Estates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "QRCode",
                table: "Estates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Security",
                table: "Estates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Shomineh",
                table: "Estates",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbNama",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "Alachigh",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "Camera",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "Fibr",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "Flower",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "QRCode",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "Security",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "Shomineh",
                table: "Estates");
        }
    }
}
