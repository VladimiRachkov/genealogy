using Genealogy.Data;
using Genealogy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;

namespace Genealogy.Migrations
{
    public partial class AddRoleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "Roles", 
                columns: new[] { "Id", "Name" }, 
                values: new object[] { DefaultValues.Roles.Admin.Id, "Администратор" });
            migrationBuilder.InsertData(
                table: "Roles", 
                columns: new[] { "Id", "Name" }, 
                values: new object[] { DefaultValues.Roles.User.Id, "Пользователь" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: DefaultValues.Roles.Admin.Id);
            migrationBuilder.DeleteData(
                table: "Roles", 
                keyColumn: "Id",
                keyValue: DefaultValues.Roles.User.Id);
        }
    }
}
