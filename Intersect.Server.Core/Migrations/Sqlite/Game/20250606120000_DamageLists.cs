using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Core.Migrations.Sqlite.Game
{
    /// <inheritdoc />
    public partial class DamageLists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Damages",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DamageTypes",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.Sql("UPDATE Items SET Damages = json_array(Damage), DamageTypes = json_array(DamageType);");

            migrationBuilder.DropColumn(
                name: "Damage",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DamageType",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "Damages",
                table: "Npcs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DamageTypes",
                table: "Npcs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.Sql("UPDATE Npcs SET Damages = json_array(Damage), DamageTypes = json_array(DamageType);");

            migrationBuilder.DropColumn(
                name: "Damage",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "DamageType",
                table: "Npcs");

            migrationBuilder.AddColumn<string>(
                name: "Damages",
                table: "Classes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DamageTypes",
                table: "Classes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.Sql("UPDATE Classes SET Damages = json_array(Damage), DamageTypes = json_array(DamageType);");

            migrationBuilder.DropColumn(
                name: "Damage",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "DamageType",
                table: "Classes");

            migrationBuilder.AddColumn<string>(
                name: "DamageTypes",
                table: "Spells",
                type: "TEXT",
                nullable: true);

            migrationBuilder.Sql("UPDATE Spells SET DamageTypes = json_array(DamageType);");

            migrationBuilder.DropColumn(
                name: "DamageType",
                table: "Spells");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Damage",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageType",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("UPDATE Items SET Damage = json_extract(Damages, '$[0]'), DamageType = json_extract(DamageTypes, '$[0]');");

            migrationBuilder.DropColumn(
                name: "Damages",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DamageTypes",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "Damage",
                table: "Npcs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageType",
                table: "Npcs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("UPDATE Npcs SET Damage = json_extract(Damages, '$[0]'), DamageType = json_extract(DamageTypes, '$[0]');");

            migrationBuilder.DropColumn(
                name: "Damages",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "DamageTypes",
                table: "Npcs");

            migrationBuilder.AddColumn<int>(
                name: "Damage",
                table: "Classes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageType",
                table: "Classes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("UPDATE Classes SET Damage = json_extract(Damages, '$[0]'), DamageType = json_extract(DamageTypes, '$[0]');");

            migrationBuilder.DropColumn(
                name: "Damages",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "DamageTypes",
                table: "Classes");

            migrationBuilder.AddColumn<int>(
                name: "DamageType",
                table: "Spells",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("UPDATE Spells SET DamageType = json_extract(DamageTypes, '$[0]');");

            migrationBuilder.DropColumn(
                name: "DamageTypes",
                table: "Spells");
        }
    }
}
