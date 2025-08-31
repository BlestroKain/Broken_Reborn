using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Player
{
    /// <inheritdoc />
    public partial class AddFactionplayerFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Faction",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Honor",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastFactionSwapAt",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Wings",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Player_KillLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AttackerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    VictimId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AttackerUserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    VictimUserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AttackerIp = table.Column<string>(type: "TEXT", nullable: true),
                    VictimIp = table.Column<string>(type: "TEXT", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player_KillLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_KillLogs_Players_AttackerId",
                        column: x => x.AttackerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Player_KillLogs_Players_VictimId",
                        column: x => x.VictimId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_KillLogs_AttackerId",
                table: "Player_KillLogs",
                column: "AttackerId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_KillLogs_VictimId",
                table: "Player_KillLogs",
                column: "VictimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player_KillLogs");

            migrationBuilder.DropColumn(
                name: "Faction",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Honor",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LastFactionSwapAt",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Wings",
                table: "Players");
        }
    }
}
