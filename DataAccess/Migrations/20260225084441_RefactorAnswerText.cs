using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RefactorAnswerText : Migration
    {
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "AnswerText",
				table: "Answers",
				type: "nvarchar(max)",
				unicode: false,
				nullable: false,
				oldClrType: typeof(string),
				oldType: "text",
				oldUnicode: false);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "AnswerText",
				table: "Answers",
				type: "text",
				unicode: false,
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(max)",
				oldUnicode: false);
		}
	}
}
