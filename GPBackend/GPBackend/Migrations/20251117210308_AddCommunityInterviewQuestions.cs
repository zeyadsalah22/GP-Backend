using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddCommunityInterviewQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "logo",
                table: "Companies",
                newName: "logo_url");

            migrationBuilder.CreateTable(
                name: "Community_Interview_Questions",
                columns: table => new
                {
                    question_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    question_text = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    role_type = table.Column<int>(type: "int", nullable: false),
                    question_type = table.Column<int>(type: "int", nullable: false),
                    difficulty = table.Column<int>(type: "int", nullable: false),
                    asked_count = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    answer_count = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    rowversion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Community_Interview_Questions", x => x.question_id);
                    table.ForeignKey(
                        name: "FK_CommunityInterviewQuestions_Companies",
                        column: x => x.company_id,
                        principalTable: "Companies",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CommunityInterviewQuestions_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Interview_Answers",
                columns: table => new
                {
                    answer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    answer_text = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    got_offer = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    helpful_count = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    rowversion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interview_Answers", x => x.answer_id);
                    table.ForeignKey(
                        name: "FK_InterviewAnswers_CommunityInterviewQuestions",
                        column: x => x.question_id,
                        principalTable: "Community_Interview_Questions",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterviewAnswers_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Question_Asked_By",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question_Asked_By", x => x.id);
                    table.ForeignKey(
                        name: "FK_QuestionAskedBy_CommunityInterviewQuestions",
                        column: x => x.question_id,
                        principalTable: "Community_Interview_Questions",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAskedBy_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Interview_Answer_Helpful",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    answer_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    rowversion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interview_Answer_Helpful", x => x.id);
                    table.ForeignKey(
                        name: "FK_InterviewAnswerHelpful_InterviewAnswers",
                        column: x => x.answer_id,
                        principalTable: "Interview_Answers",
                        principalColumn: "answer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterviewAnswerHelpful_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Community_Interview_Questions_asked_count",
                table: "Community_Interview_Questions",
                column: "asked_count");

            migrationBuilder.CreateIndex(
                name: "IX_Community_Interview_Questions_company_id",
                table: "Community_Interview_Questions",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Community_Interview_Questions_created_at",
                table: "Community_Interview_Questions",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_Community_Interview_Questions_difficulty",
                table: "Community_Interview_Questions",
                column: "difficulty");

            migrationBuilder.CreateIndex(
                name: "IX_Community_Interview_Questions_question_type",
                table: "Community_Interview_Questions",
                column: "question_type");

            migrationBuilder.CreateIndex(
                name: "IX_Community_Interview_Questions_role_type",
                table: "Community_Interview_Questions",
                column: "role_type");

            migrationBuilder.CreateIndex(
                name: "IX_Community_Interview_Questions_user_id",
                table: "Community_Interview_Questions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Interview_Answer_Helpful_answer_id",
                table: "Interview_Answer_Helpful",
                column: "answer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Interview_Answer_Helpful_answer_id_user_id",
                table: "Interview_Answer_Helpful",
                columns: new[] { "answer_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interview_Answer_Helpful_user_id",
                table: "Interview_Answer_Helpful",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Interview_Answers_helpful_count",
                table: "Interview_Answers",
                column: "helpful_count");

            migrationBuilder.CreateIndex(
                name: "IX_Interview_Answers_question_id",
                table: "Interview_Answers",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_Interview_Answers_user_id",
                table: "Interview_Answers",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Question_Asked_By_question_id",
                table: "Question_Asked_By",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_Question_Asked_By_question_id_user_id",
                table: "Question_Asked_By",
                columns: new[] { "question_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Question_Asked_By_user_id",
                table: "Question_Asked_By",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interview_Answer_Helpful");

            migrationBuilder.DropTable(
                name: "Question_Asked_By");

            migrationBuilder.DropTable(
                name: "Interview_Answers");

            migrationBuilder.DropTable(
                name: "Community_Interview_Questions");

            migrationBuilder.RenameColumn(
                name: "logo_url",
                table: "Companies",
                newName: "logo");
        }
    }
}
