using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GPBackend.Migrations
{
    /// <inheritdoc />
    public partial class SeedIndustries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Industries",
                columns: new[] { "industry_id", "created_at", "is_deleted", "name", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Technology", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Banking & Finance", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Healthcare", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Real Estate", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Construction", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Manufacturing", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Retail & E-commerce", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Logistics & Transportation", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Consulting", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Education", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Government", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Other", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "industry_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "industry_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "industry_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "industry_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "industry_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "industry_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "industry_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "industry_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "industry_id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "industry_id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "industry_id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "industry_id",
                keyValue: 12);
        }
    }
}
