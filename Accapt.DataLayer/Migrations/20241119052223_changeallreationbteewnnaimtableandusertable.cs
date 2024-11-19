using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accapt.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class changeallreationbteewnnaimtableandusertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banks_Users_UserId",
                table: "Banks");

            migrationBuilder.DropForeignKey(
                name: "FK_Billans_Users_Id",
                table: "Billans");

            migrationBuilder.DropForeignKey(
                name: "FK_Cheks_Users_UserId",
                table: "Cheks");

            migrationBuilder.DropForeignKey(
                name: "FK_DebtorCreditors_Users_UserId",
                table: "DebtorCreditors");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDeatails_Users_UsersId",
                table: "EmployeeDeatails");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_UserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_Users_Id",
                table: "InvoiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Users_Id",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Users_UserId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_products_Users_UserId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProviderServiceLists_Users_UserId",
                table: "ProviderServiceLists");

            migrationBuilder.DropForeignKey(
                name: "FK_SallaryAndCosts_Users_UsersId",
                table: "SallaryAndCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceProviders_Users_Id",
                table: "ServiceProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_VisibleServices_Users_UserId",
                table: "VisibleServices");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_VisibleServices_UserId",
                table: "VisibleServices");

            migrationBuilder.DropIndex(
                name: "IX_ServiceProviders_Id",
                table: "ServiceProviders");

            migrationBuilder.DropIndex(
                name: "IX_SallaryAndCosts_UsersId",
                table: "SallaryAndCosts");

            migrationBuilder.DropIndex(
                name: "IX_ProviderServiceLists_UserId",
                table: "ProviderServiceLists");

            migrationBuilder.DropIndex(
                name: "IX_products_UserId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_People_UserId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_Id",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDetails_Id",
                table: "InvoiceDetails");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDeatails_UsersId",
                table: "EmployeeDeatails");

            migrationBuilder.DropIndex(
                name: "IX_DebtorCreditors_UserId",
                table: "DebtorCreditors");

            migrationBuilder.DropIndex(
                name: "IX_Cheks_UserId",
                table: "Cheks");

            migrationBuilder.DropIndex(
                name: "IX_Billans_Id",
                table: "Billans");

            migrationBuilder.DropIndex(
                name: "IX_Banks_UserId",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "VisibleServices");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "SallaryAndCosts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProviderServiceLists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "EmployeeDeatails");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cheks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Banks");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "ServiceProviders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "InvoiceDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "DebtorCreditors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Billans",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "VisibleServices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "ServiceProviders",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "SallaryAndCosts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ProviderServiceLists",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "products",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "People",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "InvoiceDetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "EmployeeDeatails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "DebtorCreditors",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Cheks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Billans",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Banks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ExpireAccessDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RealFullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VerifyCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisibleServices_UserId",
                table: "VisibleServices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviders_Id",
                table: "ServiceProviders",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SallaryAndCosts_UsersId",
                table: "SallaryAndCosts",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderServiceLists_UserId",
                table: "ProviderServiceLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_products_UserId",
                table: "products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_People_UserId",
                table: "People",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Id",
                table: "Invoices",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_Id",
                table: "InvoiceDetails",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDeatails_UsersId",
                table: "EmployeeDeatails",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_DebtorCreditors_UserId",
                table: "DebtorCreditors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cheks_UserId",
                table: "Cheks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Billans_Id",
                table: "Billans",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_UserId",
                table: "Banks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_Users_UserId",
                table: "Banks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Billans_Users_Id",
                table: "Billans",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cheks_Users_UserId",
                table: "Cheks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DebtorCreditors_Users_UserId",
                table: "DebtorCreditors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDeatails_Users_UsersId",
                table: "EmployeeDeatails",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Users_UserId",
                table: "Employees",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_Users_Id",
                table: "InvoiceDetails",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Users_Id",
                table: "Invoices",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_People_Users_UserId",
                table: "People",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_Users_UserId",
                table: "products",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderServiceLists_Users_UserId",
                table: "ProviderServiceLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SallaryAndCosts_Users_UsersId",
                table: "SallaryAndCosts",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceProviders_Users_Id",
                table: "ServiceProviders",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisibleServices_Users_UserId",
                table: "VisibleServices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
