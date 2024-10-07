using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accapt.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddVisbaleServiceTableandAddTheRaltions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisibleService",
                columns: table => new
                {
                    VisibleServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderWorkId = table.Column<int>(type: "int", nullable: false),
                    SrviceName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descriptions = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    Statuce = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateOfService = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisibleService", x => x.VisibleServiceId);
                    table.ForeignKey(
                        name: "FK_VisibleService_ProviderServiceLists_ProviderWorkId",
                        column: x => x.ProviderWorkId,
                        principalTable: "ProviderServiceLists",
                        principalColumn: "ProviderWorkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisibleService_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisibleService_ProviderWorkId",
                table: "VisibleService",
                column: "ProviderWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_VisibleService_UserId",
                table: "VisibleService",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisibleService");
        }
    }
}
