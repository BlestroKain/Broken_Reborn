using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Player
{
    /// <inheritdoc />
    public partial class MarketListingIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Market_Listings_ItemId_IsSold_ExpireAt",
                table: "Market_Listings",
                columns: new[] { "ItemId", "IsSold", "ExpireAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Market_Listings_Price_ListedAt",
                table: "Market_Listings",
                columns: new[] { "Price", "ListedAt" });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Market_Listings_Positive",
                table: "Market_Listings",
                sql: "Price > 0 AND Quantity > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Market_Listings_ItemId_IsSold_ExpireAt",
                table: "Market_Listings");

            migrationBuilder.DropIndex(
                name: "IX_Market_Listings_Price_ListedAt",
                table: "Market_Listings");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Market_Listings_Positive",
                table: "Market_Listings");
        }
    }
}
