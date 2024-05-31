using Microsoft.EntityFrameworkCore.Migrations;

namespace Pardisan.Migrations
{
    public partial class estateImagesTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstateImage_Estates_EstateId",
                table: "EstateImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstateImage",
                table: "EstateImage");

            migrationBuilder.RenameTable(
                name: "EstateImage",
                newName: "EstateImages");

            migrationBuilder.RenameIndex(
                name: "IX_EstateImage_EstateId",
                table: "EstateImages",
                newName: "IX_EstateImages_EstateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstateImages",
                table: "EstateImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EstateImages_Estates_EstateId",
                table: "EstateImages",
                column: "EstateId",
                principalTable: "Estates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstateImages_Estates_EstateId",
                table: "EstateImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstateImages",
                table: "EstateImages");

            migrationBuilder.RenameTable(
                name: "EstateImages",
                newName: "EstateImage");

            migrationBuilder.RenameIndex(
                name: "IX_EstateImages_EstateId",
                table: "EstateImage",
                newName: "IX_EstateImage_EstateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstateImage",
                table: "EstateImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EstateImage_Estates_EstateId",
                table: "EstateImage",
                column: "EstateId",
                principalTable: "Estates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
