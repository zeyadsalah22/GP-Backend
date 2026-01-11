using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyRequestsTableOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company_Requests",
                columns: table => new
                {
                    request_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    company_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    industry_id = table.Column<int>(type: "int", nullable: true),
                    linkedin_link = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    careers_link = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    request_status = table.Column<int>(type: "int", nullable: false),
                    requested_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    reviewed_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    reviewed_by_admin_id = table.Column<int>(type: "int", nullable: true),
                    rejection_reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company_Requests", x => x.request_id);
                    table.ForeignKey(
                        name: "FK_CompanyRequests_Industries",
                        column: x => x.industry_id,
                        principalTable: "Industries",
                        principalColumn: "industry_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CompanyRequests_ReviewedByAdmin",
                        column: x => x.reviewed_by_admin_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_CompanyRequests_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_Requests_company_name",
                table: "Company_Requests",
                column: "company_name");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Requests_industry_id",
                table: "Company_Requests",
                column: "industry_id");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Requests_request_status",
                table: "Company_Requests",
                column: "request_status");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Requests_requested_at",
                table: "Company_Requests",
                column: "requested_at");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Requests_reviewed_by_admin_id",
                table: "Company_Requests",
                column: "reviewed_by_admin_id");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Requests_user_id",
                table: "Company_Requests",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Company_Requests");
        }
    }
}
