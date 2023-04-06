using Microsoft.EntityFrameworkCore.Migrations;
using Genealogy.Data;
using Genealogy;

namespace Genealogy.Migrations
{
    public partial class AddedBookMetatype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Metatypes",
                columns: new[] { "Id", "Name", "Title" },
                values: new object[] { MetatypeData.Book.Id, ProductData.Book.Name, "Книга" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Metatypes",
                keyColumn: "Id",
                keyValue: MetatypeData.Book.Id);
        }
    }
}
