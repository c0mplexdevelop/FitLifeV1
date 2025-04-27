using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitLife.Migrations
{
    /// <inheritdoc />
    public partial class FixoftheFixPrimaryKeyOfUserExerciseMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserExerciseHistory",
                table: "UserExerciseHistory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserExerciseHistory",
                table: "UserExerciseHistory",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserExerciseHistory_UserId",
                table: "UserExerciseHistory",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserExerciseHistory",
                table: "UserExerciseHistory");

            migrationBuilder.DropIndex(
                name: "IX_UserExerciseHistory_UserId",
                table: "UserExerciseHistory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserExerciseHistory",
                table: "UserExerciseHistory",
                columns: new[] { "UserId", "ExerciseId" });
        }
    }
}
