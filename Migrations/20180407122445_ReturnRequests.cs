using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Library.Migrations
{
    public partial class ReturnRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReturnRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Approved = table.Column<bool>(nullable: false),
                    UserBookId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnRequest_UserBook_UserBookId",
                        column: x => x.UserBookId,
                        principalTable: "UserBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReturnRequest_UserBookId",
                table: "ReturnRequest",
                column: "UserBookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReturnRequest");
        }
    }
}
