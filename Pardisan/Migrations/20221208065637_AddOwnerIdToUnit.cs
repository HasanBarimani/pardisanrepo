using Microsoft.EntityFrameworkCore.Migrations;

namespace Pardisan.Migrations
{
    public partial class AddOwnerIdToUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Units",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Owners_OwnerId",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_OwnerId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Units");
        }
    }
}
