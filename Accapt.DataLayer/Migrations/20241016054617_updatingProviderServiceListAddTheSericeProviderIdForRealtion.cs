using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accapt.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class updatingProviderServiceListAddTheSericeProviderIdForRealtion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisibleService_ProviderServiceLists_ProviderWorkId",
                table: "VisibleService");

            migrationBuilder.DropForeignKey(
                name: "FK_VisibleService_Users_UserId",
                table: "VisibleService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisibleService",
                table: "VisibleService");

            migrationBuilder.RenameTable(
                name: "VisibleService",
                newName: "VisibleServices");

            migrationBuilder.RenameIndex(
                name: "IX_VisibleService_UserId",
                table: "VisibleServices",
                newName: "IX_VisibleServices_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_VisibleService_ProviderWorkId",
                table: "VisibleServices",
                newName: "IX_VisibleServices_ProviderWorkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisibleServices",
                table: "VisibleServices",
                column: "VisibleServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisibleServices_ProviderServiceLists_ProviderWorkId",
                table: "VisibleServices",
                column: "ProviderWorkId",
                principalTable: "ProviderServiceLists",
                principalColumn: "ProviderWorkId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisibleServices_Users_UserId",
                table: "VisibleServices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisibleServices_ProviderServiceLists_ProviderWorkId",
                table: "VisibleServices");

            migrationBuilder.DropForeignKey(
                name: "FK_VisibleServices_Users_UserId",
                table: "VisibleServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisibleServices",
                table: "VisibleServices");

            migrationBuilder.RenameTable(
                name: "VisibleServices",
                newName: "VisibleService");

            migrationBuilder.RenameIndex(
                name: "IX_VisibleServices_UserId",
                table: "VisibleService",
                newName: "IX_VisibleService_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_VisibleServices_ProviderWorkId",
                table: "VisibleService",
                newName: "IX_VisibleService_ProviderWorkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisibleService",
                table: "VisibleService",
                column: "VisibleServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisibleService_ProviderServiceLists_ProviderWorkId",
                table: "VisibleService",
                column: "ProviderWorkId",
                principalTable: "ProviderServiceLists",
                principalColumn: "ProviderWorkId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisibleService_Users_UserId",
                table: "VisibleService",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
