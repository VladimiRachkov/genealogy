using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Genealogy.Migrations
{
    public partial class AddedCounty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CountyId",
                table: "Cemeteries",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "County",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    isRemoved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_County", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cemeteries_CountyId",
                table: "Cemeteries",
                column: "CountyId");

            // migrationBuilder.CreateIndex(
            //     name: "IX_BusinessObjects_UserId",
            //     table: "BusinessObjects",
            //     column: "UserId");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_BusinessObjects_Users_UserId",
            //     table: "BusinessObjects",
            //     column: "UserId",
            //     principalTable: "Users",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cemeteries_County_CountyId",
                table: "Cemeteries",
                column: "CountyId",
                principalTable: "County",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_BusinessObjects_Users_UserId",
            //     table: "BusinessObjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Cemeteries_County_CountyId",
                table: "Cemeteries");

            migrationBuilder.DropTable(
                name: "County");

            migrationBuilder.DropIndex(
                name: "IX_Cemeteries_CountyId",
                table: "Cemeteries");

            // migrationBuilder.DropIndex(
            //     name: "IX_BusinessObjects_UserId",
            //     table: "BusinessObjects");

            migrationBuilder.DropColumn(
                name: "CountyId",
                table: "Cemeteries");
        }
    }
}
