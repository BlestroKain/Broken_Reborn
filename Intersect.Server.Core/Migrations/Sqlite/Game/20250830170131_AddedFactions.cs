using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Game
{
    /// <inheritdoc />
    public partial class AddedFactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Faction",
                table: "Npcs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "JailMapId",
                table: "Npcs",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<byte>(
                name: "JailX",
                table: "Npcs",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "JailY",
                table: "Npcs",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<bool>(
                name: "SendToJailOnCapture",
                table: "Npcs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Faction",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "JailMapId",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "JailX",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "JailY",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "SendToJailOnCapture",
                table: "Npcs");
        }
    }
}
