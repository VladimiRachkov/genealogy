using Microsoft.EntityFrameworkCore.Migrations;

namespace Genealogy.Migrations
{
    public partial class AddMetatypeToBusinessObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BusinessObjects_MetatypeId",
                table: "BusinessObjects",
                column: "MetatypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessObjects_Metatypes_MetatypeId",
                table: "BusinessObjects",
                column: "MetatypeId",
                principalTable: "Metatypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessObjects_Metatypes_MetatypeId",
                table: "BusinessObjects");

            migrationBuilder.DropIndex(
                name: "IX_BusinessObjects_MetatypeId",
                table: "BusinessObjects");
        }
    }
}
