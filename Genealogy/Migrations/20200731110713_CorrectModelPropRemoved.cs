using Microsoft.EntityFrameworkCore.Migrations;

namespace Genealogy.Migrations
{
    public partial class CorrectModelPropRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isRemoved",
                table: "Link");

            migrationBuilder.RenameColumn(
                name: "Removed",
                table: "Persons",
                newName: "isRemoved");

            migrationBuilder.RenameColumn(
                name: "Removed",
                table: "Page",
                newName: "isRemoved");

            migrationBuilder.RenameColumn(
                name: "Removed",
                table: "Cemeteries",
                newName: "isRemoved");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isRemoved",
                table: "Persons",
                newName: "Removed");

            migrationBuilder.RenameColumn(
                name: "isRemoved",
                table: "Page",
                newName: "Removed");

            migrationBuilder.RenameColumn(
                name: "isRemoved",
                table: "Cemeteries",
                newName: "Removed");

            migrationBuilder.AddColumn<bool>(
                name: "isRemoved",
                table: "Link",
                nullable: false,
                defaultValue: false);
        }
    }
}
