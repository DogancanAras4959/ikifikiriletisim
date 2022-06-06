using Microsoft.EntityFrameworkCore.Migrations;

namespace ikifikir.DAL.Migrations
{
    public partial class postRecycleMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "post");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "post",
                newName: "IsActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "post",
                newName: "isActive");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "post",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
