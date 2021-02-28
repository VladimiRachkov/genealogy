using Genealogy.Data;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Genealogy.Migrations
{
    public partial class AddPurchaseMetatype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Metatypes",
                columns: new[] { "Id", "Name", "Title" },
                values: new object[] { MetatypeData.Purchase.Id, "PURCHASE", "Покупка" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Metatypes",
                keyColumn: "Id",
                keyValue: MetatypeData.Purchase.Id);
        }
    }
}
