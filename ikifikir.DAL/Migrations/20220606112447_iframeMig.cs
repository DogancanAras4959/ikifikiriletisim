using Microsoft.EntityFrameworkCore.Migrations;

namespace ikifikir.DAL.Migrations
{
    public partial class iframeMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "videoSlug",
                table: "project");

            migrationBuilder.AddColumn<string>(
                name: "iframe",
                table: "videos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iframe",
                table: "videos");

            migrationBuilder.AddColumn<string>(
                name: "videoSlug",
                table: "project",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
