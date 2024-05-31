using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pardisan.Migrations
{
    public partial class sdvkl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AparatLink",
                table: "Holdings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "History",
                table: "Holdings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MapLink",
                table: "Holdings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Target",
                table: "Holdings",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HoldingImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    HoldingId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoldingImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoldingImage_Holdings_HoldingId",
                        column: x => x.HoldingId,
                        principalTable: "Holdings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HoldingImage_HoldingId",
                table: "HoldingImage",
                column: "HoldingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoldingImage");

            migrationBuilder.DropColumn(
                name: "AparatLink",
                table: "Holdings");

            migrationBuilder.DropColumn(
                name: "History",
                table: "Holdings");

            migrationBuilder.DropColumn(
                name: "MapLink",
                table: "Holdings");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "Holdings");
        }
    }
}
