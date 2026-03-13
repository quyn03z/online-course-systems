using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveQuestionTypeTypeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuestionType_QuestionTypeTypeId",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionTypeTypeId",
                table: "Questions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TypeId",
                table: "Questions",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuestionType_QuestionTypeTypeId",
                table: "Questions",
                column: "QuestionTypeTypeId",
                principalTable: "QuestionType",
                principalColumn: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuestionType_TypeId",
                table: "Questions",
                column: "TypeId",
                principalTable: "QuestionType",
                principalColumn: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuestionType_QuestionTypeTypeId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuestionType_TypeId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_TypeId",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionTypeTypeId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuestionType_QuestionTypeTypeId",
                table: "Questions",
                column: "QuestionTypeTypeId",
                principalTable: "QuestionType",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
