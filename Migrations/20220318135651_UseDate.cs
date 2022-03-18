using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscountCodes6.Migrations
{
    public partial class UseDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Used",
                table: "DiscountUses",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Used",
                table: "DiscountUses");
        }
    }
}
