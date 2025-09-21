using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Game
{
    /// <inheritdoc />
    public partial class AddedPetnewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanEvolve",
                table: "Pets",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "EvolutionLevel",
                table: "Pets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "EvolutionTarget",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "ExperienceRate",
                table: "Pets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LevelingMode",
                table: "Pets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxLevel",
                table: "Pets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatPointsPerLevel",
                table: "Pets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanEvolve",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "EvolutionLevel",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "EvolutionTarget",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "ExperienceRate",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "LevelingMode",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "MaxLevel",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "StatPointsPerLevel",
                table: "Pets");
        }
    }
}
