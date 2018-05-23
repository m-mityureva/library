using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Library.Migrations
{
    public partial class UserBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Shipped",
                table: "Orders",
                newName: "Approved");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Books",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserBook",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookId = table.Column<int>(nullable: true),
                    Returned = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBook_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBook_BookId",
                table: "UserBook",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBook");

            migrationBuilder.RenameColumn(
                name: "Approved",
                table: "Orders",
                newName: "Shipped");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Books",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
