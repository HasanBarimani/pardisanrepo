using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pardisan.Migrations
{
    public partial class AddSurveyOwners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Surveys",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SurveyOwners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: true),
                    OwnerId = table.Column<int>(nullable: false),
                    SurveyId = table.Column<int>(nullable: false),
                    HasAnswered = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyOwners_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyOwners_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: true),
                    SurveyId = table.Column<int>(nullable: true),
                    QuestionId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    ChosenOptions = table.Column<string>(nullable: true),
                    SurveyOwnerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Answers_SurveyOwners_SurveyOwnerId",
                        column: x => x.SurveyOwnerId,
                        principalTable: "SurveyOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_PropertyId",
                table: "Surveys",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_OwnerId",
                table: "Answers",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_SurveyId",
                table: "Answers",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_SurveyOwnerId",
                table: "Answers",
                column: "SurveyOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyOwners_OwnerId",
                table: "SurveyOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyOwners_SurveyId",
                table: "SurveyOwners",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Properties_PropertyId",
                table: "Surveys",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Properties_PropertyId",
                table: "Surveys");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "SurveyOwners");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_PropertyId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Surveys");
        }
    }
}
