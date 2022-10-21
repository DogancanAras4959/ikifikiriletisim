using Microsoft.EntityFrameworkCore.Migrations;

namespace ikifikir.DAL.Migrations
{
    public partial class v1Calculate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "priceTitle",
                table: "pricingComponents");

            migrationBuilder.AddColumn<string>(
                name: "priceLongTitle",
                table: "pricing",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "priceLongTitle",
                table: "pricing");

            migrationBuilder.AddColumn<string>(
                name: "priceTitle",
                table: "pricingComponents",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
