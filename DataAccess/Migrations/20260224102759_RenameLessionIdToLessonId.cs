using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenameLessionIdToLessonId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizz_Lesson_LessionId",
                table: "Quizz");

            migrationBuilder.DropForeignKey(
                name: "FK_SubLesson_Lesson_LessionId",
                table: "SubLesson");

            migrationBuilder.RenameColumn(
                name: "LessionId",
                table: "SubLesson",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_SubLesson_LessionId",
                table: "SubLesson",
                newName: "IX_SubLesson_LessonId");

            migrationBuilder.RenameColumn(
                name: "LessionName",
                table: "Quizz",
                newName: "LessonName");

            migrationBuilder.RenameColumn(
                name: "LessionId",
                table: "Quizz",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Quizz_LessionId",
                table: "Quizz",
                newName: "IX_Quizz_LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizz_Lesson_LessonId",
                table: "Quizz",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubLesson_Lesson_LessonId",
                table: "SubLesson",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizz_Lesson_LessonId",
                table: "Quizz");

            migrationBuilder.DropForeignKey(
                name: "FK_SubLesson_Lesson_LessonId",
                table: "SubLesson");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "SubLesson",
                newName: "LessionId");

            migrationBuilder.RenameIndex(
                name: "IX_SubLesson_LessonId",
                table: "SubLesson",
                newName: "IX_SubLesson_LessionId");

            migrationBuilder.RenameColumn(
                name: "LessonName",
                table: "Quizz",
                newName: "LessionName");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "Quizz",
                newName: "LessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Quizz_LessonId",
                table: "Quizz",
                newName: "IX_Quizz_LessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizz_Lesson_LessionId",
                table: "Quizz",
                column: "LessionId",
                principalTable: "Lesson",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubLesson_Lesson_LessionId",
                table: "SubLesson",
                column: "LessionId",
                principalTable: "Lesson",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
