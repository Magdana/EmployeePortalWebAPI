using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeePortalWebAPI.Migrations
{
    public partial class renameEmployee2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employees_EmployeeEntityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmployeeEntityId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "EmployeeEntityId",
                table: "Users",
                newName: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeId",
                table: "Users",
                column: "EmployeeId",
                unique: true,
                filter: "[EmployeeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employees_EmployeeId",
                table: "Users",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employees_EmployeeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmployeeId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Users",
                newName: "EmployeeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeEntityId",
                table: "Users",
                column: "EmployeeEntityId",
                unique: true,
                filter: "[EmployeeEntityId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employees_EmployeeEntityId",
                table: "Users",
                column: "EmployeeEntityId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
