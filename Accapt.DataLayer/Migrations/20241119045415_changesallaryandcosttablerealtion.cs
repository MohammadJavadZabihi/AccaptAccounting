using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accapt.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class changesallaryandcosttablerealtion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SallaryAndCosts_Users_UserId",
                table: "SallaryAndCosts");

            migrationBuilder.DropIndex(
                name: "IX_SallaryAndCosts_UserId",
                table: "SallaryAndCosts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SallaryAndCosts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "SallaryAndCosts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SallaryAndCosts_UsersId",
                table: "SallaryAndCosts",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_SallaryAndCosts_Users_UsersId",
                table: "SallaryAndCosts",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SallaryAndCosts_Users_UsersId",
                table: "SallaryAndCosts");

            migrationBuilder.DropIndex(
                name: "IX_SallaryAndCosts_UsersId",
                table: "SallaryAndCosts");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "SallaryAndCosts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SallaryAndCosts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_SallaryAndCosts_UserId",
                table: "SallaryAndCosts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SallaryAndCosts_Users_UserId",
                table: "SallaryAndCosts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
