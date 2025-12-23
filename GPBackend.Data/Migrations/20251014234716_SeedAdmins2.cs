using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GPBackend.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdmins2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "user_id", "address", "birth_date", "created_at", "email", "fname", "is_deleted", "lname", "password", "role", "updated_at" },
                values: new object[,]
                {
                    { 3, null, null, new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin3@gmail.com", "Admin", false, "Admin3", "O04maomAXJ0CD5rKZjitY+hwH8jHXAyhlS0UBU0fEM8=", 1, new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, null, null, new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin4@gmail.com", "Admin", false, "Admin4", "O04maomAXJ0CD5rKZjitY+hwH8jHXAyhlS0UBU0fEM8=", 1, new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "user_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "user_id",
                keyValue: 4);
        }
    }
}
