using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accapt.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddProviderNameInVisibleServiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProviderName",
                table: "VisibleServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProviderName",
                table: "VisibleServices");
        }
    }
}
