using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppSquareTask.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBoatTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Boats");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Boats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "MaxCancellationPeriod",
                table: "Boats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerPerson",
                table: "Boats",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Boats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "MaxCancellationPeriod",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "PricePerPerson",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Boats");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Boats",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
