using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddSavedPostsFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Saved_Posts",
                columns: table => new
                {
                    saved_post_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    post_id = table.Column<int>(type: "int", nullable: false),
                    saved_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    rowversion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saved_Posts", x => x.saved_post_id);
                    table.ForeignKey(
                        name: "FK_SavedPosts_Posts",
                        column: x => x.post_id,
                        principalTable: "Posts",
                        principalColumn: "post_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavedPosts_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Saved_Posts_post_id",
                table: "Saved_Posts",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_Saved_Posts_user_id",
                table: "Saved_Posts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Saved_Posts_user_id_post_id",
                table: "Saved_Posts",
                columns: new[] { "user_id", "post_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Saved_Posts");
        }
    }
}
