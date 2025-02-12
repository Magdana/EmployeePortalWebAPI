using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeePortalWebAPI.Migrations
{
    public partial class addOneToOneConnectionOfUserAndEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeEntityId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employees_EmployeeEntityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmployeeEntityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmployeeEntityId",
                table: "Users");
        }
    }
}
