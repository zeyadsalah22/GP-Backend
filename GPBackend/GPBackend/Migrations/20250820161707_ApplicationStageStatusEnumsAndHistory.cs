using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPBackend.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationStageStatusEnumsAndHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create temp columns for safe data migration
            migrationBuilder.AddColumn<int>(
                name: "status_tmp",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "stage_tmp",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Map existing string values into enum ints
            migrationBuilder.Sql(@"
                UPDATE A SET A.status_tmp = CASE LOWER(A.status)
                    WHEN 'accepted' THEN 0
                    WHEN 'pending' THEN 1
                    WHEN 'rejected' THEN 2
                    ELSE 1 END
                FROM Applications A;

                UPDATE A SET A.stage_tmp = CASE LOWER(A.stage)
                    WHEN 'applied' THEN 0
                    WHEN 'phonescreen' THEN 1
                    WHEN 'assessment' THEN 2
                    WHEN 'interview' THEN 3
                    WHEN 'offer' THEN 4
                    ELSE 0 END
                FROM Applications A;
            ");

            // Drop old columns and rename temp to original names
            migrationBuilder.DropColumn(
                name: "status",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "status_tmp",
                table: "Applications",
                newName: "status");

            migrationBuilder.DropColumn(
                name: "stage",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "stage_tmp",
                table: "Applications",
                newName: "stage");

            migrationBuilder.CreateTable(
                name: "Application_Stage_History",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    application_id = table.Column<int>(type: "int", nullable: false),
                    stage = table.Column<int>(type: "int", nullable: false),
                    reached_date = table.Column<DateOnly>(type: "date", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    rowversion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application_Stage_History", x => x.id);
                    table.ForeignKey(
                        name: "FK_AppStageHistory_Applications",
                        column: x => x.application_id,
                        principalTable: "Applications",
                        principalColumn: "application_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Application_Stage_History_application_id_stage",
                table: "Application_Stage_History",
                columns: new[] { "application_id", "stage" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Application_Stage_History");

            // Revert enum columns back to strings (best-effort)
            migrationBuilder.AddColumn<string>(
                name: "status_old",
                table: "Applications",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "pending");

            migrationBuilder.AddColumn<string>(
                name: "stage_old",
                table: "Applications",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "applied");

            migrationBuilder.Sql(@"
                UPDATE A SET A.status_old = CASE A.status
                    WHEN 0 THEN 'accepted'
                    WHEN 1 THEN 'pending'
                    WHEN 2 THEN 'rejected'
                    ELSE 'pending' END
                FROM Applications A;

                UPDATE A SET A.stage_old = CASE A.stage
                    WHEN 0 THEN 'applied'
                    WHEN 1 THEN 'phonescreen'
                    WHEN 2 THEN 'assessment'
                    WHEN 3 THEN 'interview'
                    WHEN 4 THEN 'offer'
                    ELSE 'applied' END
                FROM Applications A;
            ");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Applications");
            migrationBuilder.RenameColumn(
                name: "status_old",
                table: "Applications",
                newName: "status");

            migrationBuilder.DropColumn(
                name: "stage",
                table: "Applications");
            migrationBuilder.RenameColumn(
                name: "stage_old",
                table: "Applications",
                newName: "stage");
        }
    }
}
