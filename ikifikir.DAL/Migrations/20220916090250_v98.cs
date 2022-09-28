using Microsoft.EntityFrameworkCore.Migrations;

namespace ikifikir.DAL.Migrations
{
    public partial class v98 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChooseType",
                table: "pricingComponentTypes");

            migrationBuilder.AddColumn<bool>(
                name: "ChooseType",
                table: "pricingComponents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChooseType",
                table: "pricingComponents");

            migrationBuilder.AddColumn<bool>(
                name: "ChooseType",
                table: "pricingComponentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
