using Genealogy.Data;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Genealogy.Migrations
{
    public partial class AddMetatypeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Metatypes",
                columns: new[] { "Id", "Name", "Title" },
                values: new object[] { MetatypeData.Product.Id, "product", "Продукт" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
