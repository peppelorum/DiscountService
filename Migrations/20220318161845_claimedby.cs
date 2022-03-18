using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscountCodes6.Migrations
{
    public partial class claimedby : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountUses");

            migrationBuilder.AddColumn<Guid>(
                name: "ClaimedByUserId",
                table: "DiscountCodes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ClaimedDate",
                table: "DiscountCodes",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimedByUserId",
                table: "DiscountCodes");

            migrationBuilder.DropColumn(
                name: "ClaimedDate",
                table: "DiscountCodes");

            migrationBuilder.CreateTable(
                name: "DiscountUses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DiscountCodeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Used = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountUses", x => x.Id);
                });
        }
    }
}
