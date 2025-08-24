using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Game
{
    /// <inheritdoc />
    public partial class AddedSetSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Set",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "SetDescription",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SetName",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ItemIds = table.Column<string>(type: "TEXT", nullable: true),
                    VitalsGiven = table.Column<string>(type: "TEXT", nullable: true),
                    VitalsRegen = table.Column<string>(type: "TEXT", nullable: true),
                    PercentageVitalsGiven = table.Column<string>(type: "TEXT", nullable: true),
                    StatsGiven = table.Column<string>(type: "TEXT", nullable: true),
                    PercentageStatsGiven = table.Column<string>(type: "TEXT", nullable: true),
                    Effects = table.Column<string>(type: "TEXT", nullable: true),
                    Folder = table.Column<string>(type: "TEXT", nullable: true),
                    TimeCreated = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sets");

            migrationBuilder.DropColumn(
                name: "Set",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SetDescription",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SetName",
                table: "Items");
        }
    }
}
