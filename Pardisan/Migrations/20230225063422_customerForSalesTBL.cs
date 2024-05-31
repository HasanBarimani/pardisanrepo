using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pardisan.Migrations
{
    public partial class customerForSalesTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerForSales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<int>(nullable: false),
                    Demand = table.Column<string>(nullable: true),
                    DocumentType = table.Column<string>(nullable: true),
                    Meterage = table.Column<string>(nullable: true),
                    Passage = table.Column<string>(nullable: true),
                    LandBar = table.Column<string>(nullable: true),
                    HowToKnow = table.Column<string>(nullable: true),
                    Providers = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PropertyAddress = table.Column<string>(nullable: true),
                    Usgae = table.Column<string>(nullable: true),
                    FirstRecord = table.Column<string>(nullable: true),
                    SecondRecord = table.Column<string>(nullable: true),
                    SalesManagerOpinion = table.Column<string>(nullable: true),
                    FinalOpinion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerForSales", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerForSales");
        }
    }
}
