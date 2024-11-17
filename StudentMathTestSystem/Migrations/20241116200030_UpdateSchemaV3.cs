using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentMathTestSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchemaV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherID",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExamTaskID",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentID",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExamID",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherID",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "ExamTaskID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ExamID",
                table: "Exams");
        }
    }
}
