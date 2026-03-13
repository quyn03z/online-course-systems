using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RefactorQuizzIdQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizz_QuizId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "QuizId",
                table: "Questions",
                newName: "QuizzId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_QuizId",
                table: "Questions",
                newName: "IX_Questions_QuizzId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizz_QuizzId",
                table: "Questions",
                column: "QuizzId",
                principalTable: "Quizz",
                principalColumn: "QuizzId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizz_QuizzId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "QuizzId",
                table: "Questions",
                newName: "QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_QuizzId",
                table: "Questions",
                newName: "IX_Questions_QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizz_QuizId",
                table: "Questions",
                column: "QuizId",
                principalTable: "Quizz",
                principalColumn: "QuizzId");
        }
    }
}
