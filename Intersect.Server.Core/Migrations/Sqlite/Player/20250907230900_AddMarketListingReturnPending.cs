using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Player
{
    /// <inheritdoc />
    public partial class AddMarketListingReturnPending : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ReturnPending",
                table: "Market_Listings",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Market_Listings_ReturnPending",
                table: "Market_Listings",
                column: "ReturnPending");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Market_Listings_ReturnPending",
                table: "Market_Listings");

            migrationBuilder.DropColumn(
                name: "ReturnPending",
                table: "Market_Listings");
        }
    }
}
