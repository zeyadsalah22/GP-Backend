using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPBackend.Migrations
{
    /// <inheritdoc />
    public partial class UserCompanyEnhancements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description",
                table: "User_Companies",
                newName: "personal_notes");

            migrationBuilder.AddColumn<bool>(
                name: "favorite",
                table: "User_Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "interest_level",
                table: "User_Companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserCompany_Tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    tag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompany_Tags", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserCompanyTags_UserCompanies",
                        columns: x => new { x.user_id, x.company_id },
                        principalTable: "User_Companies",
                        principalColumns: new[] { "user_id", "company_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCompany_Tags_user_id_company_id_tag",
                table: "UserCompany_Tags",
                columns: new[] { "user_id", "company_id", "tag" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCompany_Tags");

            migrationBuilder.DropColumn(
                name: "favorite",
                table: "User_Companies");

            migrationBuilder.DropColumn(
                name: "interest_level",
                table: "User_Companies");

            migrationBuilder.RenameColumn(
                name: "personal_notes",
                table: "User_Companies",
                newName: "description");
        }
    }
}
