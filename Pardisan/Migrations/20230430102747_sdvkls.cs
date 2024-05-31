using Microsoft.EntityFrameworkCore.Migrations;

namespace Pardisan.Migrations
{
    public partial class sdvkls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoldingImage_Holdings_HoldingId",
                table: "HoldingImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HoldingImage",
                table: "HoldingImage");

            migrationBuilder.RenameTable(
                name: "HoldingImage",
                newName: "HoldingImages");

            migrationBuilder.RenameIndex(
                name: "IX_HoldingImage_HoldingId",
                table: "HoldingImages",
                newName: "IX_HoldingImages_HoldingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HoldingImages",
                table: "HoldingImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HoldingImages_Holdings_HoldingId",
                table: "HoldingImages",
                column: "HoldingId",
                principalTable: "Holdings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoldingImages_Holdings_HoldingId",
                table: "HoldingImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HoldingImages",
                table: "HoldingImages");

            migrationBuilder.RenameTable(
                name: "HoldingImages",
                newName: "HoldingImage");

            migrationBuilder.RenameIndex(
                name: "IX_HoldingImages_HoldingId",
                table: "HoldingImage",
                newName: "IX_HoldingImage_HoldingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HoldingImage",
                table: "HoldingImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HoldingImage_Holdings_HoldingId",
                table: "HoldingImage",
                column: "HoldingId",
                principalTable: "Holdings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
