using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.MySql.Player
{
    public partial class GuildLogos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoBackground",
                table: "Guilds",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BackgroundR",
                table: "Guilds",
                type: "int",
                nullable: false,
                defaultValue: 255);

            migrationBuilder.AddColumn<int>(
                name: "BackgroundG",
                table: "Guilds",
                type: "int",
                nullable: false,
                defaultValue: 255);

            migrationBuilder.AddColumn<int>(
                name: "BackgroundB",
                table: "Guilds",
                type: "int",
                nullable: false,
                defaultValue: 255);

            migrationBuilder.AddColumn<string>(
                name: "LogoSymbol",
                table: "Guilds",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SymbolR",
                table: "Guilds",
                type: "int",
                nullable: false,
                defaultValue: 255);

            migrationBuilder.AddColumn<int>(
                name: "SymbolG",
                table: "Guilds",
                type: "int",
                nullable: false,
                defaultValue: 255);

            migrationBuilder.AddColumn<int>(
                name: "SymbolB",
                table: "Guilds",
                type: "int",
                nullable: false,
                defaultValue: 255);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "LogoBackground", table: "Guilds");
            migrationBuilder.DropColumn(name: "BackgroundR", table: "Guilds");
            migrationBuilder.DropColumn(name: "BackgroundG", table: "Guilds");
            migrationBuilder.DropColumn(name: "BackgroundB", table: "Guilds");
            migrationBuilder.DropColumn(name: "LogoSymbol", table: "Guilds");
            migrationBuilder.DropColumn(name: "SymbolR", table: "Guilds");
            migrationBuilder.DropColumn(name: "SymbolG", table: "Guilds");
            migrationBuilder.DropColumn(name: "SymbolB", table: "Guilds");
        }
    }
}
