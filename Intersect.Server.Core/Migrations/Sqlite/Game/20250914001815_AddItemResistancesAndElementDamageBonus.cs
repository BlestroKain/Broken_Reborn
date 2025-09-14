using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Game
{
    /// <inheritdoc />
    public partial class AddItemResistancesAndElementDamageBonus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Resistances",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: "{}");

            migrationBuilder.AddColumn<string>(
                name: "ElementDamageBonus",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: "{}");

            migrationBuilder.Sql("UPDATE Items SET Resistances='{}' WHERE Resistances IS NULL OR Resistances='';");
            migrationBuilder.Sql("UPDATE Items SET ElementDamageBonus='{}' WHERE ElementDamageBonus IS NULL OR ElementDamageBonus='';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resistances",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ElementDamageBonus",
                table: "Items");
        }
    }
}
