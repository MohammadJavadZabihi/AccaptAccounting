using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accapt.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class initiamigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.CreateTable(
                name: "SallaryAndCosts",
                columns: table => new
                {
                    SallaryAndCostsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SallaryAndCostsName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PriceOfSallaryAndCosts = table.Column<double>(type: "float", nullable: false),
                    DateOfSubmit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfSubmitForShow = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descriptions = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SallaryAndCosts", x => x.SallaryAndCostsId);
                    table.ForeignKey(
                        name: "FK_SallaryAndCosts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SallaryAndCosts_UserId",
                table: "SallaryAndCosts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.DropTable(
                name: "SallaryAndCosts");
        }
    }
}
