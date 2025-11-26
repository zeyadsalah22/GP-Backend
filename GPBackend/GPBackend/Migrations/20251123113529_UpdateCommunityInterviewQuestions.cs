using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCommunityInterviewQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "added_question_type",
                table: "Community_Interview_Questions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "added_role_type",
                table: "Community_Interview_Questions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "company_logo",
                table: "Community_Interview_Questions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "company_name",
                table: "Community_Interview_Questions",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "added_question_type",
                table: "Community_Interview_Questions");

            migrationBuilder.DropColumn(
                name: "added_role_type",
                table: "Community_Interview_Questions");

            migrationBuilder.DropColumn(
                name: "company_logo",
                table: "Community_Interview_Questions");

            migrationBuilder.DropColumn(
                name: "company_name",
                table: "Community_Interview_Questions");
        }
    }
}
