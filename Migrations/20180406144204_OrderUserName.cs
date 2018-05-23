using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Library.Migrations
{
    public partial class OrderUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Orders",
                nullable: false,
                defaultValue: "");
        }
    }
}
