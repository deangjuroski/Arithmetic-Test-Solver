using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentMathTestSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentResultColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Exams_ExamID",
                table: "Tasks");

            migrationBuilder.AlterColumn<double>(
                name: "ExpectedResult",
                table: "Tasks",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ExamID",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "StudentResult",
                table: "Tasks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Exams_ExamID",
                table: "Tasks",
                column: "ExamID",
                principalTable: "Exams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Exams_ExamID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "StudentResult",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "ExpectedResult",
                table: "Tasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "ExamID",
                table: "Tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Exams_ExamID",
                table: "Tasks",
                column: "ExamID",
                principalTable: "Exams",
                principalColumn: "ID");
        }
    }
}
