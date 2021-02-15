using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Genealogy.Migrations
{
    public partial class AddBusinessObjectModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessObjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    MetatypeId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    FinishDate = table.Column<DateTime>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    Data = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessObjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinkRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Metatype = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    FinishDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkRelations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Metatypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metatypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObjectRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BusinessObjectId = table.Column<Guid>(nullable: false),
                    LinkRelationId = table.Column<Guid>(nullable: false),
                    MetatypeId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    FinishDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectRelations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessObjects");

            migrationBuilder.DropTable(
                name: "LinkRelations");

            migrationBuilder.DropTable(
                name: "Metatypes");

            migrationBuilder.DropTable(
                name: "ObjectRelations");
        }
    }
}
