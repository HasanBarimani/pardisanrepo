using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pardisan.Migrations
{
    public partial class CustomerFormTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerForms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumer = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    CallSubject = table.Column<string>(nullable: true),
                    HowToKnow = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    MyProperty = table.Column<int>(nullable: false),
                    Transactions = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerForms", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerForms");
        }
    }
}
