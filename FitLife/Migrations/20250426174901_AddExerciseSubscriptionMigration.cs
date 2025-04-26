using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitLife.Migrations
{
    /// <inheritdoc />
    public partial class AddExerciseSubscriptionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserExerciseSubscriptions",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExerciseSubscriptions", x => new { x.UserId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_UserExerciseSubscriptions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserExerciseSubscriptions_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserExerciseSubscriptions_ExerciseId",
                table: "UserExerciseSubscriptions",
                column: "ExerciseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserExerciseSubscriptions");
        }
    }
}
