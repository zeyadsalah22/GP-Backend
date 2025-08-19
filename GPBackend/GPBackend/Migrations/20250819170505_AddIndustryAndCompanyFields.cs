using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddIndustryAndCompanyFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Industries table first
            migrationBuilder.CreateTable(
                name: "Industries",
                columns: table => new
                {
                    industry_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    rowversion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industries", x => x.industry_id);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_Industries_Name",
                table: "Industries",
                column: "name",
                unique: true);

            // Seed a default 'Unknown' industry and capture its Id
            migrationBuilder.Sql("INSERT INTO Industries(name, created_at, updated_at, is_deleted) VALUES('Unknown', SYSUTCDATETIME(), SYSUTCDATETIME(), 0);");

            migrationBuilder.AddColumn<int>(
                name: "company_size",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "industry_id",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "logo",
                table: "Companies",
                type: "varbinary(max)",
                nullable: true);

            // Set existing rows to the 'Unknown' industry
            migrationBuilder.Sql("UPDATE Companies SET industry_id = (SELECT TOP 1 industry_id FROM Industries WHERE name = 'Unknown') WHERE industry_id = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_industry_id",
                table: "Companies",
                column: "industry_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Industries",
                table: "Companies",
                column: "industry_id",
                principalTable: "Industries",
                principalColumn: "industry_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Industries",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "Industries");

            migrationBuilder.DropIndex(
                name: "IX_Companies_industry_id",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "company_size",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "industry_id",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "logo",
                table: "Companies");
        }
    }
}
