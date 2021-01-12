using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Genealogy.Migrations
{
    public partial class AddPersonGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PersonGroupId",
                table: "Persons",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PersonGroupId",
                table: "Persons",
                column: "PersonGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_PersonGroups_PersonGroupId",
                table: "Persons",
                column: "PersonGroupId",
                principalTable: "PersonGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_PersonGroups_PersonGroupId",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "PersonGroups");

            migrationBuilder.DropIndex(
                name: "IX_Persons_PersonGroupId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PersonGroupId",
                table: "Persons");
        }
    }
}
