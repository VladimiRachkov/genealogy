using Genealogy.Data;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Genealogy.Migrations
{
    public partial class AddMessageMetatype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Metatypes",
                columns: new[] { "Id", "Name", "Title" },
                values: new object[] { MetatypeData.Message.Id, "MESSAGE", "Сообщение" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Metatypes",
                keyColumn: "Id",
                keyValue: MetatypeData.Message.Id);
        }
    }
}
