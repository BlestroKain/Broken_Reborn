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

            migrationBuilder.CreateTable(
                name: "Sets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ItemIds = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "[]"),
                    VitalsGiven = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "[]"),
                    VitalsRegen = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "[]"),
                    PercentageVitalsGiven = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "[]"),
                    StatsGiven = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "[]"),
                    PercentageStatsGiven = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "[]"),
                    Effects = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "[]"),
                    Folder = table.Column<string>(type: "TEXT", nullable: true),
                    TimeCreated = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_Set",
                table: "Items",
                column: "Set");

            // When a set is deleted, items referencing it should become unset.
            // Use SET DEFAULT so the Set column reverts to Guid.Empty instead of restricting the delete.
            migrationBuilder.AddForeignKey(
                name: "FK_Items_Sets_Set",
                table: "Items",
                column: "Set",
                principalTable: "Sets",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetDefault);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Sets_Set",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Set",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Sets");

            migrationBuilder.DropColumn(
                name: "Set",
                table: "Items");
        }
    }
}
