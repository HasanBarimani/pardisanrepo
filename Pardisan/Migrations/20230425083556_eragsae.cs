using Microsoft.EntityFrameworkCore.Migrations;

namespace Pardisan.Migrations
{
    public partial class eragsae : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_PropertyOwnersList_OwnersId1",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_OwnersId1",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "OwnersId1",
                table: "Units");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyOwnersList_UnitId",
                table: "PropertyOwnersList",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyOwnersList_Units_UnitId",
                table: "PropertyOwnersList",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyOwnersList_Units_UnitId",
                table: "PropertyOwnersList");

            migrationBuilder.DropIndex(
                name: "IX_PropertyOwnersList_UnitId",
                table: "PropertyOwnersList");

            migrationBuilder.AddColumn<int>(
                name: "OwnersId1",
                table: "Units",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Units_OwnersId1",
                table: "Units",
                column: "OwnersId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_PropertyOwnersList_OwnersId1",
                table: "Units",
                column: "OwnersId1",
                principalTable: "PropertyOwnersList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
