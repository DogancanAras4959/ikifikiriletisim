using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ikifikir.DAL.Migrations
{
    public partial class postMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tagProjects_project_projectId",
                table: "tagProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_tagProjects_tags_tagId",
                table: "tagProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tagProjects",
                table: "tagProjects");

            migrationBuilder.RenameTable(
                name: "tagProjects",
                newName: "tagProject");

            migrationBuilder.RenameIndex(
                name: "IX_tagProjects_tagId",
                table: "tagProject",
                newName: "IX_tagProject_tagId");

            migrationBuilder.RenameIndex(
                name: "IX_tagProjects_projectId",
                table: "tagProject",
                newName: "IX_tagProject_projectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tagProject",
                table: "tagProject",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    spot = table.Column<string>(type: "nvarchar(130)", maxLength: 130, nullable: true),
                    content = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    sorted = table.Column<int>(type: "int", nullable: false),
                    seoTitle = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    seoSpot = table.Column<string>(type: "nvarchar(130)", maxLength: 130, nullable: true),
                    keywords = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    author = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_tagProject_project_projectId",
                table: "tagProject",
                column: "projectId",
                principalTable: "project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tagProject_tags_tagId",
                table: "tagProject",
                column: "tagId",
                principalTable: "tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tagProject_project_projectId",
                table: "tagProject");

            migrationBuilder.DropForeignKey(
                name: "FK_tagProject_tags_tagId",
                table: "tagProject");

            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tagProject",
                table: "tagProject");

            migrationBuilder.RenameTable(
                name: "tagProject",
                newName: "tagProjects");

            migrationBuilder.RenameIndex(
                name: "IX_tagProject_tagId",
                table: "tagProjects",
                newName: "IX_tagProjects_tagId");

            migrationBuilder.RenameIndex(
                name: "IX_tagProject_projectId",
                table: "tagProjects",
                newName: "IX_tagProjects_projectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tagProjects",
                table: "tagProjects",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tagProjects_project_projectId",
                table: "tagProjects",
                column: "projectId",
                principalTable: "project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tagProjects_tags_tagId",
                table: "tagProjects",
                column: "tagId",
                principalTable: "tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
