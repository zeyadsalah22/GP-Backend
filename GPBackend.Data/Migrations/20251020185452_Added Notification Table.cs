using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedNotificationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    notification_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    actor_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    entity_targeted_id = table.Column<int>(type: "int", nullable: true),
                    message = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    is_read = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.notification_id);
                    table.ForeignKey(
                        name: "FK_Notifications_Actor",
                        column: x => x.actor_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Notifications_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_actor_id",
                table: "Notifications",
                column: "actor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_user_id",
                table: "Notifications",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
