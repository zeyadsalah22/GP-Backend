using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPBackend.Migrations
{
    /// <inheritdoc />
    public partial class resettttt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PasswordResetToken_Users_UserId",
                table: "PasswordResetToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PasswordResetToken",
                table: "PasswordResetToken");

            migrationBuilder.RenameTable(
                name: "PasswordResetToken",
                newName: "PasswordResetTokens");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PasswordResetTokens",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PasswordResetTokens",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "UsedAt",
                table: "PasswordResetTokens",
                newName: "used_at");

            migrationBuilder.RenameColumn(
                name: "TokenHash",
                table: "PasswordResetTokens",
                newName: "token_hash");

            migrationBuilder.RenameColumn(
                name: "ExpiresAt",
                table: "PasswordResetTokens",
                newName: "expires_at");

            migrationBuilder.RenameColumn(
                name: "CreatedUserAgent",
                table: "PasswordResetTokens",
                newName: "created_user_agent");

            migrationBuilder.RenameColumn(
                name: "CreatedIp",
                table: "PasswordResetTokens",
                newName: "created_ip");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "PasswordResetTokens",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_PasswordResetToken_UserId",
                table: "PasswordResetTokens",
                newName: "IX_PasswordResetTokens_user_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "used_at",
                table: "PasswordResetTokens",
                type: "datetime2(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "token_hash",
                table: "PasswordResetTokens",
                type: "varbinary(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "expires_at",
                table: "PasswordResetTokens",
                type: "datetime2(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "PasswordResetTokens",
                type: "datetime2(0)",
                precision: 0,
                nullable: false,
                defaultValueSql: "(sysutcdatetime())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PasswordResetTokens",
                table: "PasswordResetTokens",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_token_hash",
                table: "PasswordResetTokens",
                column: "token_hash",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordResetTokens_Users",
                table: "PasswordResetTokens",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PasswordResetTokens_Users",
                table: "PasswordResetTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PasswordResetTokens",
                table: "PasswordResetTokens");

            migrationBuilder.DropIndex(
                name: "IX_PasswordResetTokens_token_hash",
                table: "PasswordResetTokens");

            migrationBuilder.RenameTable(
                name: "PasswordResetTokens",
                newName: "PasswordResetToken");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PasswordResetToken",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "PasswordResetToken",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "used_at",
                table: "PasswordResetToken",
                newName: "UsedAt");

            migrationBuilder.RenameColumn(
                name: "token_hash",
                table: "PasswordResetToken",
                newName: "TokenHash");

            migrationBuilder.RenameColumn(
                name: "expires_at",
                table: "PasswordResetToken",
                newName: "ExpiresAt");

            migrationBuilder.RenameColumn(
                name: "created_user_agent",
                table: "PasswordResetToken",
                newName: "CreatedUserAgent");

            migrationBuilder.RenameColumn(
                name: "created_ip",
                table: "PasswordResetToken",
                newName: "CreatedIp");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "PasswordResetToken",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_PasswordResetTokens_user_id",
                table: "PasswordResetToken",
                newName: "IX_PasswordResetToken_UserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UsedAt",
                table: "PasswordResetToken",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "TokenHash",
                table: "PasswordResetToken",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiresAt",
                table: "PasswordResetToken",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldPrecision: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "PasswordResetToken",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldPrecision: 0,
                oldDefaultValueSql: "(sysutcdatetime())");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PasswordResetToken",
                table: "PasswordResetToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordResetToken_Users_UserId",
                table: "PasswordResetToken",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
