using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LoginApp.Migrations
{
    public partial class NameDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Regex",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Regex",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Regex");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Regex");
        }
    }
}
