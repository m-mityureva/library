using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Library.Migrations
{
    public partial class QuantityUserBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_UserBook_CartLine_CartLineID",
                table: "UserBook");

            migrationBuilder.RenameColumn(
                name: "CartLineID",
                table: "UserBook",
                newName: "BookId");
                
            migrationBuilder.RenameIndex(
                name: "IX_UserBook_CartLineID",
                table: "UserBook",
                newName: "IX_UserBook_BookId");
*/
            migrationBuilder.AlterColumn<int>(
                name: "Returned",
                table: "UserBook",
                nullable: false,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<DateTime>(
                name: "IssuanceDate",
                table: "UserBook",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "UserBook",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ReturnRequest",
                nullable: false,
                defaultValue: 0);

            /*migrationBuilder.AddForeignKey(
                name: "FK_UserBook_Books_BookId",
                table: "UserBook",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_UserBook_Books_BookId",
                table: "UserBook");*/

            migrationBuilder.DropColumn(
                name: "IssuanceDate",
                table: "UserBook");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "UserBook");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ReturnRequest");

            /*migrationBuilder.RenameColumn(
                name: "BookId",
                table: "UserBook",
                newName: "CartLineID");

            migrationBuilder.RenameIndex(
                name: "IX_UserBook_BookId",
                table: "UserBook",
                newName: "IX_UserBook_CartLineID");*/

            migrationBuilder.AlterColumn<bool>(
                name: "Returned",
                table: "UserBook",
                nullable: false,
                oldClrType: typeof(int));

            /*migrationBuilder.AddForeignKey(
                name: "FK_UserBook_CartLine_CartLineID",
                table: "UserBook",
                column: "CartLineID",
                principalTable: "CartLine",
                principalColumn: "CartLineID",
                onDelete: ReferentialAction.Restrict);*/
        }
    }
}
