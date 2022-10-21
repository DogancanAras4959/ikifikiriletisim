using Microsoft.EntityFrameworkCore.Migrations;

namespace ikifikir.DAL.Migrations
{
    public partial class vCalculate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "pricingComponents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "priceTitle",
                table: "pricingComponents",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "pricingComponents");

            migrationBuilder.DropColumn(
                name: "priceTitle",
                table: "pricingComponents");
        }
    }
}
