using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BCNPortal.Migrations
{
    public partial class AddFieldTokenTble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BcnUserId",
                table: "Token",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "PortalUserId",
                table: "Token",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BcnUserId",
                table: "Token");

            migrationBuilder.DropColumn(
                name: "PortalUserId",
                table: "Token");
        }
    }
}
