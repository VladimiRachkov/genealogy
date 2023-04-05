using Microsoft.EntityFrameworkCore.Migrations;
using Genealogy.Data;

namespace Genealogy.Migrations
{
    public partial class AddedLastLogMetatype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Metatypes",
                columns: new[] { "Id", "Name", "Title" },
                values: new object[] { Logs.LastLog.Id, Logs.LastLog.Name, "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Metatypes",
                keyColumn: "Id",
                keyValue: Logs.LastLog.Id);
        }
    }
}
