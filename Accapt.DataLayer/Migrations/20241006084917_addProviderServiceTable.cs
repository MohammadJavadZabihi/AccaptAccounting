using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accapt.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class addProviderServiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceProviders",
                columns: table => new
                {
                    ServiceProviderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ProviderPassword = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsCreditor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AmountOfCreditor = table.Column<double>(type: "float", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProviders", x => x.ServiceProviderId);
                    table.ForeignKey(
                        name: "FK_ServiceProviders_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProviderServiceLists",
                columns: table => new
                {
                    ProviderWorkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                    DateOfService = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfServiceForShow = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Descriptions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ServiceProviderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderServiceLists", x => x.ProviderWorkId);
                    table.ForeignKey(
                        name: "FK_ProviderServiceLists_ServiceProviders_ServiceProviderId",
                        column: x => x.ServiceProviderId,
                        principalTable: "ServiceProviders",
                        principalColumn: "ServiceProviderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProviderServiceLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProviderServiceLists_ServiceProviderId",
                table: "ProviderServiceLists",
                column: "ServiceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderServiceLists_UserId",
                table: "ProviderServiceLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviders_Id",
                table: "ServiceProviders",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProviderServiceLists");

            migrationBuilder.DropTable(
                name: "ServiceProviders");
        }
    }
}
