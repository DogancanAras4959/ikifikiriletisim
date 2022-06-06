using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ikifikir.DAL.Migrations
{
    public partial class projectMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    seoTitle = table.Column<string>(type: "nvarchar(130)", maxLength: 130, nullable: true),
                    seoDescription = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    projectName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    projectSpot = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    parentProjectId = table.Column<int>(type: "int", nullable: false),
                    client = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    projectDate = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    website = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    imageThumbnail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    sorted = table.Column<int>(type: "int", nullable: false),
                    videoSlug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isTitle = table.Column<bool>(type: "bit", nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_project_category_categoryId",
                        column: x => x.categoryId,
                        principalTable: "category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_project_categoryId",
                table: "project",
                column: "categoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "project");

            migrationBuilder.DropTable(
                name: "category");
        }
    }
}
