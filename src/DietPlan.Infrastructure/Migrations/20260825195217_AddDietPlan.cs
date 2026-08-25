using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DietPlan.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDietPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DietPlanId",
                table: "Meals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DietPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DailyCalories = table.Column<double>(type: "float", nullable: false),
                    ProteinGrams = table.Column<double>(type: "float", nullable: false),
                    CarbohydrateGrams = table.Column<double>(type: "float", nullable: false),
                    FatGrams = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietPlans_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meals_DietPlanId",
                table: "Meals",
                column: "DietPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPlans_UserProfileId",
                table: "DietPlans",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_DietPlans_DietPlanId",
                table: "Meals",
                column: "DietPlanId",
                principalTable: "DietPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_DietPlans_DietPlanId",
                table: "Meals");

            migrationBuilder.DropTable(
                name: "DietPlans");

            migrationBuilder.DropIndex(
                name: "IX_Meals_DietPlanId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "DietPlanId",
                table: "Meals");
        }
    }
}
