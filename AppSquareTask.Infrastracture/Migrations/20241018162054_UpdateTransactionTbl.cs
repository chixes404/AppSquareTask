using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppSquareTask.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTransactionTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Wallet_WalletId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WalletId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MaxCancellationPeriod",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "AdditionalServices",
                table: "BoatBookings");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PricePerPerson",
                table: "Boats",
                newName: "Price");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "TripBookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRefunded",
                table: "TripBookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "serviceId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "BoatBookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRefunded",
                table: "BoatBookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "BoatBookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_UserId",
                table: "Wallet",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_AspNetUsers_UserId",
                table: "Wallet",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_AspNetUsers_UserId",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_UserId",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "TripBookings");

            migrationBuilder.DropColumn(
                name: "IsRefunded",
                table: "TripBookings");

            migrationBuilder.DropColumn(
                name: "serviceId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "BoatBookings");

            migrationBuilder.DropColumn(
                name: "IsRefunded",
                table: "BoatBookings");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BoatBookings");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Boats",
                newName: "PricePerPerson");

            migrationBuilder.AddColumn<DateTime>(
                name: "MaxCancellationPeriod",
                table: "Boats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AdditionalServices",
                table: "BoatBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

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
        }
    }
}
