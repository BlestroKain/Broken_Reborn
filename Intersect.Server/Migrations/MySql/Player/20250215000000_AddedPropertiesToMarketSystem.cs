using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.MySql.Player
{
    /// <inheritdoc />
    public partial class AddedPropertiesToMarketSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemProperties",
                table: "MarketListings",
                type: "longtext",
                nullable: false,
                defaultValue: "{}" );

            migrationBuilder.AddColumn<string>(
                name: "ItemProperties",
                table: "MarketTransactions",
                type: "longtext",
                nullable: false,
                defaultValue: "{}" );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemProperties",
                table: "MarketListings" );

            migrationBuilder.DropColumn(
                name: "ItemProperties",
                table: "MarketTransactions" );
        }
    }
}
