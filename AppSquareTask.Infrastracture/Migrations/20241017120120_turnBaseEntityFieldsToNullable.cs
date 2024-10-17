using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppSquareTask.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class turnBaseEntityFieldsToNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Owners_WalletId",
                table: "Owners",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_WalletId",
                table: "Customers",
                column: "WalletId");

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
    }
}
