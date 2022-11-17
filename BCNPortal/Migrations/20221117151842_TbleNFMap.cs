using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BCNPortal.Migrations
{
    public partial class TbleNFMap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NFbaseAdd",
                table: "NFmapping",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NFbaseAdd",
                table: "NFmapping");
        }
    }
}
