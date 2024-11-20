using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accapt.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class addupdateclientupdatetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientUpdates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsMandentory = table.Column<bool>(type: "bit", nullable: false),
                    RealeseNote = table.Column<string>(type: "nvarchar(900)", maxLength: 900, nullable: false),
                    DownloadUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientUpdates", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientUpdates");
        }
    }
}
