using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddInterviewFeedbackPersistence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interview_Question_Feedback",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    score = table.Column<double>(type: "float", nullable: false),
                    feedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    strengths_json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    improvements_json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    context = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    raw_response_json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interview_Question_Feedback", x => x.id);
                    table.ForeignKey(
                        name: "FK_Interview_Question_Feedback_Interview_Questions",
                        column: x => x.question_id,
                        principalTable: "Interview_Questions",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interview_Video_Feedback",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    interview_id = table.Column<int>(type: "int", nullable: false),
                    confidence = table.Column<double>(type: "float", nullable: false),
                    engagement = table.Column<double>(type: "float", nullable: false),
                    stress = table.Column<double>(type: "float", nullable: false),
                    authenticity = table.Column<double>(type: "float", nullable: false),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strengths_json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    improvements_json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    recommendations_json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    report_path = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    raw_response_json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interview_Video_Feedback", x => x.id);
                    table.ForeignKey(
                        name: "FK_Interview_Video_Feedback_Interviews",
                        column: x => x.interview_id,
                        principalTable: "Interviews",
                        principalColumn: "interview_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interview_Question_Feedback_question_id",
                table: "Interview_Question_Feedback",
                column: "question_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interview_Video_Feedback_interview_id",
                table: "Interview_Video_Feedback",
                column: "interview_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interview_Question_Feedback");

            migrationBuilder.DropTable(
                name: "Interview_Video_Feedback");
        }
    }
}
