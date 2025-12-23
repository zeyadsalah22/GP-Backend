using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddGmailListenerTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailApplicationUpdates",
                columns: table => new
                {
                    EmailApplicationUpdateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmailSubject = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EmailFrom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmailDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailSnippet = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DetectedStatus = table.Column<int>(type: "int", nullable: true),
                    DetectedStage = table.Column<int>(type: "int", nullable: true),
                    Confidence = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyNameHint = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    WasApplied = table.Column<bool>(type: "bit", nullable: false),
                    FailureReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailApplicationUpdates", x => x.EmailApplicationUpdateId);
                    table.ForeignKey(
                        name: "FK_EmailApplicationUpdates_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "application_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailApplicationUpdates_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GmailConnections",
                columns: table => new
                {
                    GmailConnectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EncryptedAccessToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EncryptedRefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GmailAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastCheckedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ConnectedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Rowversion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GmailConnections", x => x.GmailConnectionId);
                    table.ForeignKey(
                        name: "FK_GmailConnections_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailApplicationUpdates_ApplicationId",
                table: "EmailApplicationUpdates",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailApplicationUpdates_UserId",
                table: "EmailApplicationUpdates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GmailConnections_UserId",
                table: "GmailConnections",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailApplicationUpdates");

            migrationBuilder.DropTable(
                name: "GmailConnections");
        }
    }
}
