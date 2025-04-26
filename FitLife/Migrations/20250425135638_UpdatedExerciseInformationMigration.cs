using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitLife.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedExerciseInformationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "Reps",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "Sets",
                table: "Exercises");

            migrationBuilder.AddColumn<string>(
                name: "Equipments",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxDurationMinutes",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxReps",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxSets",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinDurationMinutes",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinReps",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinSets",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Equipments",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "MaxDurationMinutes",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "MaxReps",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "MaxSets",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "MinDurationMinutes",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "MinReps",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "MinSets",
                table: "Exercises");

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Reps",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sets",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
