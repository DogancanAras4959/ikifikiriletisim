using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ikifikir.DAL.Migrations
{
    public partial class v78 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pricing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    MonthPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    YearPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pricing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pricingComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponentTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PricingId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pricingComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pricingComponents_pricing_PricingId",
                        column: x => x.PricingId,
                        principalTable: "pricing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pricingComponentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChooseType = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    pricingComponentId = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pricingComponentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pricingComponentTypes_pricingComponents_pricingComponentId",
                        column: x => x.pricingComponentId,
                        principalTable: "pricingComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pricingComponents_PricingId",
                table: "pricingComponents",
                column: "PricingId");

            migrationBuilder.CreateIndex(
                name: "IX_pricingComponentTypes_pricingComponentId",
                table: "pricingComponentTypes",
                column: "pricingComponentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pricingComponentTypes");

            migrationBuilder.DropTable(
                name: "pricingComponents");

            migrationBuilder.DropTable(
                name: "pricing");
        }
    }
}
