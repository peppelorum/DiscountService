using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscountCodes6.Migrations
{
    public partial class Codes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codes_Capacity",
                table: "DiscountCodeJobs");

            migrationBuilder.AddColumn<string>(
                name: "Codes",
                table: "DiscountCodeJobs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codes",
                table: "DiscountCodeJobs");

            migrationBuilder.AddColumn<int>(
                name: "Codes_Capacity",
                table: "DiscountCodeJobs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
