using Microsoft.EntityFrameworkCore.Migrations;

namespace Genealogy.Migrations
{
    public partial class AddedCountyCoords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Coords",
                table: "County",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coords",
                table: "County");
        }
    }
}
