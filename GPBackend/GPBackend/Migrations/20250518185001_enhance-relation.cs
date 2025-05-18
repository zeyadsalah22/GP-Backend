using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPBackend.Migrations
{
    /// <inheritdoc />
    public partial class enhancerelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_user_id",
                table: "Employees");

            //migrationBuilder.AlterColumn<byte[]>(
            //    name: "rowversion",
            //    table: "Companies",
            //    type: "rowversion",
            //    rowVersion: true,
            //    nullable: true,
            //    oldClrType: typeof(byte[]),
            //    oldType: "rowversion",
            //    oldRowVersion: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<byte[]>(
            //    name: "rowversion",
            //    table: "Companies",
            //    type: "rowversion",
            //    rowVersion: true,
            //    nullable: false,
            //    defaultValue: new byte[0],
            //    oldClrType: typeof(byte[]),
            //    oldType: "rowversion",
            //    oldRowVersion: true,
            //    oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Users_user_id",
                table: "Employees",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
