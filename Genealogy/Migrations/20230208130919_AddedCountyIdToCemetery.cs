using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Genealogy.Migrations
{
    public partial class AddedCountyIdToCemetery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cemeteries_County_CountyId",
                table: "Cemeteries");

            migrationBuilder.AlterColumn<Guid>(
                name: "CountyId",
                table: "Cemeteries",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cemeteries_County_CountyId",
                table: "Cemeteries",
                column: "CountyId",
                principalTable: "County",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cemeteries_County_CountyId",
                table: "Cemeteries");

            migrationBuilder.AlterColumn<Guid>(
                name: "CountyId",
                table: "Cemeteries",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Cemeteries_County_CountyId",
                table: "Cemeteries",
                column: "CountyId",
                principalTable: "County",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
