using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Player
{
    /// <inheritdoc />
    public partial class AddedMarketSystemCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Market_Listings_ReturnPending",
                table: "Market_Listings");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Market_Transactions");

            migrationBuilder.DropColumn(
                name: "ItemProperties",
                table: "Market_Transactions");

            migrationBuilder.DropColumn(
                name: "ReturnPending",
                table: "Market_Listings");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Market_Listings");

            migrationBuilder.RenameColumn(
                name: "TransactionTime",
                table: "Market_Transactions",
                newName: "SoldAt");

            migrationBuilder.AlterColumn<string>(
                name: "BuyerName",
                table: "Market_Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SoldAt",
                table: "Market_Transactions",
                newName: "TransactionTime");

            migrationBuilder.AlterColumn<string>(
                name: "BuyerName",
                table: "Market_Transactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "BuyerId",
                table: "Market_Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemProperties",
                table: "Market_Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReturnPending",
                table: "Market_Listings",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Market_Listings",
                type: "BLOB",
                rowVersion: true,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Market_Listings_ReturnPending",
                table: "Market_Listings",
                column: "ReturnPending");
        }
    }
}
