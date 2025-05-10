using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPBackend.Migrations
{
    /// <inheritdoc />
    public partial class addAnnotation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "TodoLists");

            migrationBuilder.AddColumn<DateTime>(
                name: "deadline",
                table: "TodoLists",
                type: "datetime2",
                nullable: true);

            

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Users_user_id",
                table: "Employees",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_user_id",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "deadline",
                table: "TodoLists");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "TodoLists",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
