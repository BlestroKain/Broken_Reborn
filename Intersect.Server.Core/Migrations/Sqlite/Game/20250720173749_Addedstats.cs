using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Game
{
    /// <inheritdoc />
    public partial class Addedstats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatRange_MagicResist_LowRange",
                table: "Items_EquipmentProperties",
                newName: "StatRange_Vitality_LowRange");

            migrationBuilder.RenameColumn(
                name: "StatRange_MagicResist_HighRange",
                table: "Items_EquipmentProperties",
                newName: "StatRange_Vitality_HighRange");

            migrationBuilder.RenameColumn(
                name: "StatRange_AbilityPower_LowRange",
                table: "Items_EquipmentProperties",
                newName: "StatRange_Intelligence_LowRange");

            migrationBuilder.RenameColumn(
                name: "StatRange_AbilityPower_HighRange",
                table: "Items_EquipmentProperties",
                newName: "StatRange_Intelligence_HighRange");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatRange_Vitality_LowRange",
                table: "Items_EquipmentProperties",
                newName: "StatRange_MagicResist_LowRange");

            migrationBuilder.RenameColumn(
                name: "StatRange_Vitality_HighRange",
                table: "Items_EquipmentProperties",
                newName: "StatRange_MagicResist_HighRange");

            migrationBuilder.RenameColumn(
                name: "StatRange_Intelligence_LowRange",
                table: "Items_EquipmentProperties",
                newName: "StatRange_AbilityPower_LowRange");

            migrationBuilder.RenameColumn(
                name: "StatRange_Intelligence_HighRange",
                table: "Items_EquipmentProperties",
                newName: "StatRange_AbilityPower_HighRange");
        }
    }
}
