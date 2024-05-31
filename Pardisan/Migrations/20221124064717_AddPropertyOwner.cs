using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pardisan.Migrations
{
    public partial class AddPropertyOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyOwners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: true),
                    OwnerId = table.Column<int>(nullable: false),
                    PropertyId = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: true),
                    IsUnitOwnership = table.Column<bool>(nullable: false),
                    ContractSigningDate = table.Column<DateTime>(nullable: false),
                    SatisfactionLevel = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    HasDesireToIntroduce = table.Column<bool>(nullable: false),
                    HasIntroductionLeadsToPurchase = table.Column<bool>(nullable: false),
                    DisSatisfactionLevelReason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyOwners_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyOwners_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyOwners_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyOwners_OwnerId",
                table: "PropertyOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyOwners_PropertyId",
                table: "PropertyOwners",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyOwners_UnitId",
                table: "PropertyOwners",
                column: "UnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyOwners");
        }
    }
}
