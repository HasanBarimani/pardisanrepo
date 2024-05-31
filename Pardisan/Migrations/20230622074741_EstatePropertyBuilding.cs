using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pardisan.Migrations
{
    public partial class EstatePropertyBuilding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FloorCounts",
                table: "Estates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Floors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: true),
                    EstateId = table.Column<int>(nullable: false),
                    FloorNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Floors_Estates_EstateId",
                        column: x => x.EstateId,
                        principalTable: "Estates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstateUnits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: true),
                    FloorsId = table.Column<int>(nullable: true),
                    FloorId = table.Column<int>(nullable: false),
                    MergedUnitId = table.Column<int>(nullable: true),
                    IsEmpty = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstateUnits_Floors_FloorsId",
                        column: x => x.FloorsId,
                        principalTable: "Floors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstateUnits_FloorsId",
                table: "EstateUnits",
                column: "FloorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_EstateId",
                table: "Floors",
                column: "EstateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstateUnits");

            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.DropColumn(
                name: "FloorCounts",
                table: "Estates");
        }
    }
}
