using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppSquareTask.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class updateWalletTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_Customers_CustomerId",
                table: "Wallet");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_Owners_ownerId",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_CustomerId",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_ownerId",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "ownerId",
                table: "Wallet");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Wallet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "Owners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owners_WalletId",
                table: "Owners",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_WalletId",
                table: "Customers",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WalletId",
                table: "AspNetUsers",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Wallet_WalletId",
                table: "AspNetUsers",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Wallet_WalletId",
                table: "Customers",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_Wallet_WalletId",
                table: "Owners",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Wallet_WalletId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Wallet_WalletId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Owners_Wallet_WalletId",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Owners_WalletId",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Customers_WalletId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WalletId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Wallet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ownerId",
                table: "Wallet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_CustomerId",
                table: "Wallet",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_ownerId",
                table: "Wallet",
                column: "ownerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_Customers_CustomerId",
                table: "Wallet",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_Owners_ownerId",
                table: "Wallet",
                column: "ownerId",
                principalTable: "Owners",
                principalColumn: "Id");
        }
    }
}
