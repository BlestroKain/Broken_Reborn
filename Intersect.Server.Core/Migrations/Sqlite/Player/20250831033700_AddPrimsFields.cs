using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Player
{
    /// <inheritdoc />
    public partial class AddPrimsFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrentBattleId",
                table: "Prisms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdleAnimationId",
                table: "Prisms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastHitAt",
                table: "Prisms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MaturationEndsAt",
                table: "Prisms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PlacedAt",
                table: "Prisms",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SpriteOffsetY",
                table: "Prisms",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Prisms",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "TintByFaction",
                table: "Prisms",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UnderAttackAnimationId",
                table: "Prisms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VulnerableAnimationId",
                table: "Prisms",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentBattleId",
                table: "Prisms");

            migrationBuilder.DropColumn(
                name: "IdleAnimationId",
                table: "Prisms");

            migrationBuilder.DropColumn(
                name: "LastHitAt",
                table: "Prisms");

            migrationBuilder.DropColumn(
                name: "MaturationEndsAt",
                table: "Prisms");

            migrationBuilder.DropColumn(
                name: "PlacedAt",
                table: "Prisms");

            migrationBuilder.DropColumn(
                name: "SpriteOffsetY",
                table: "Prisms");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Prisms");

            migrationBuilder.DropColumn(
                name: "TintByFaction",
                table: "Prisms");

            migrationBuilder.DropColumn(
                name: "UnderAttackAnimationId",
                table: "Prisms");

            migrationBuilder.DropColumn(
                name: "VulnerableAnimationId",
                table: "Prisms");
        }
    }
}
