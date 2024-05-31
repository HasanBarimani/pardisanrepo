using Microsoft.EntityFrameworkCore.Migrations;

namespace Pardisan.Migrations
{
    public partial class dropmapLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MapAddres",
                table: "Estates");

            migrationBuilder.AddColumn<double>(
                name: "Lat",
                table: "Estates",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Long",
                table: "Estates",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "Estates");

            migrationBuilder.AddColumn<string>(
                name: "MapAddres",
                table: "Estates",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
