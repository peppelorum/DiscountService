using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscountCodes6.Migrations
{
    public partial class Uses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscountCodeJobs",
                table: "DiscountCodeJobs");

            migrationBuilder.RenameTable(
                name: "DiscountCodeJobs",
                newName: "DiscountCodeJob");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscountCodeJob",
                table: "DiscountCodeJob",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DiscountUses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DiscountCodeId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountUses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountUses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscountCodeJob",
                table: "DiscountCodeJob");

            migrationBuilder.RenameTable(
                name: "DiscountCodeJob",
                newName: "DiscountCodeJobs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscountCodeJobs",
                table: "DiscountCodeJobs",
                column: "Id");
        }
    }
}
