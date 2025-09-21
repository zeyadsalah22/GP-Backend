using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPBackend.Migrations
{
    /// <inheritdoc />
    public partial class QuestionEnhancementsAndTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "answer_status",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "difficulty",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "favorite",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "preparation_note",
                table: "Questions",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Question_Tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    tag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question_Tags", x => x.id);
                    table.ForeignKey(
                        name: "FK_QuestionTags_Questions",
                        column: x => x.question_id,
                        principalTable: "Questions",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_Tags_question_id_tag",
                table: "Question_Tags",
                columns: new[] { "question_id", "tag" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Question_Tags");

            migrationBuilder.DropColumn(
                name: "answer_status",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "difficulty",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "favorite",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "preparation_note",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "type",
                table: "Questions");
        }
    }
}
