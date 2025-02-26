﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ikifikir.DAL.Migrations
{
    public partial class v3Calculate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "pricingComponents",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "pricingComponents");
        }
    }
}
