using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Game
{
    /// <inheritdoc />
    public partial class Addstats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatRange_Agility_HighRange",
                table: "Items_EquipmentProperties",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatRange_Agility_LowRange",
                table: "Items_EquipmentProperties",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatRange_Cures_HighRange",
                table: "Items_EquipmentProperties",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatRange_Cures_LowRange",
                table: "Items_EquipmentProperties",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatRange_Damages_HighRange",
                table: "Items_EquipmentProperties",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatRange_Damages_LowRange",
                table: "Items_EquipmentProperties",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatRange_Agility_HighRange",
                table: "Items_EquipmentProperties");

            migrationBuilder.DropColumn(
                name: "StatRange_Agility_LowRange",
                table: "Items_EquipmentProperties");

            migrationBuilder.DropColumn(
                name: "StatRange_Cures_HighRange",
                table: "Items_EquipmentProperties");

            migrationBuilder.DropColumn(
                name: "StatRange_Cures_LowRange",
                table: "Items_EquipmentProperties");

            migrationBuilder.DropColumn(
                name: "StatRange_Damages_HighRange",
                table: "Items_EquipmentProperties");

            migrationBuilder.DropColumn(
                name: "StatRange_Damages_LowRange",
                table: "Items_EquipmentProperties");
        }
    }
}
