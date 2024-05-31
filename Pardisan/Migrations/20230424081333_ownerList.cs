using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pardisan.Migrations
{
    public partial class ownerList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Owners_OwnerId",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_OwnerId",
                table: "Units");

            migrationBuilder.CreateTable(
                name: "PropertyOwnersList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: true),
                    UnitId = table.Column<int>(nullable: true),
                    OwnersId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyOwnersList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyOwnersList_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyOwnersList_UnitId",
                table: "PropertyOwnersList",
                column: "UnitId",
                unique: true,
                filter: "[UnitId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyOwnersList");

            migrationBuilder.CreateIndex(
                name: "IX_Units_OwnerId",
                table: "Units",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Owners_OwnerId",
                table: "Units",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
