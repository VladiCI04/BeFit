using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeFit.Data.Migrations
{
    public partial class AddCoachCategoryToCoach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coaches_CoachCategories_CoachCategoryId",
                table: "Coaches");

            migrationBuilder.AlterColumn<int>(
                name: "CoachCategoryId",
                table: "Coaches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Coaches_CoachCategories_CoachCategoryId",
                table: "Coaches",
                column: "CoachCategoryId",
                principalTable: "CoachCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coaches_CoachCategories_CoachCategoryId",
                table: "Coaches");

            migrationBuilder.AlterColumn<int>(
                name: "CoachCategoryId",
                table: "Coaches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Coaches_CoachCategories_CoachCategoryId",
                table: "Coaches",
                column: "CoachCategoryId",
                principalTable: "CoachCategories",
                principalColumn: "Id");
        }
    }
}
