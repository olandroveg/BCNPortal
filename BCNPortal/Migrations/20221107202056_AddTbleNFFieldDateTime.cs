﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BCNPortal.Migrations
{
    public partial class AddTbleNFFieldDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "NFmapping",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "NFmapping");
        }
    }
}
