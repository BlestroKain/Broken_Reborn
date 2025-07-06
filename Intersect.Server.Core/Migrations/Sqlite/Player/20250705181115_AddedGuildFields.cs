using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Player
{
    /// <inheritdoc />
    public partial class AddedGuildFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DonateXPGuild",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<float>(
                name: "GuildExpPercentage",
                table: "Players",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<byte>(
                name: "BackgroundB",
                table: "Guilds",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "BackgroundG",
                table: "Guilds",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "BackgroundR",
                table: "Guilds",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<long>(
                name: "Experience",
                table: "Guilds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "GuildPoints",
                table: "Guilds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GuildUpgradesData",
                table: "Guilds",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Guilds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LogoBackground",
                table: "Guilds",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoSymbol",
                table: "Guilds",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpentGuildPoints",
                table: "Guilds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "SymbolB",
                table: "Guilds",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "SymbolG",
                table: "Guilds",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "SymbolR",
                table: "Guilds",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonateXPGuild",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GuildExpPercentage",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "BackgroundB",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "BackgroundG",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "BackgroundR",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "GuildPoints",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "GuildUpgradesData",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "LogoBackground",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "LogoSymbol",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "SpentGuildPoints",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "SymbolB",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "SymbolG",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "SymbolR",
                table: "Guilds");
        }
    }
}
