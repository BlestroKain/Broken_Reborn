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

            migrationBuilder.CreateTable(
                name: "Prisms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MapId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Faction = table.Column<int>(type: "INTEGER", nullable: false),
                    X = table.Column<int>(type: "INTEGER", nullable: false),
                    Y = table.Column<int>(type: "INTEGER", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Hp = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxHp = table.Column<int>(type: "INTEGER", nullable: false),
                    Windows = table.Column<string>(type: "TEXT", nullable: true),
                    Modules = table.Column<string>(type: "TEXT", nullable: true),
                    Area = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prisms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FactionAreaBonuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PrismId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Faction = table.Column<int>(type: "INTEGER", nullable: false),
                    Bonus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactionAreaBonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactionAreaBonuses_Prisms_PrismId",
                        column: x => x.PrismId,
                        principalTable: "Prisms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrismBattles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PrismId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrismBattles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrismBattles_Prisms_PrismId",
                        column: x => x.PrismId,
                        principalTable: "Prisms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrismContributions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    BattleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlayerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlayerUserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlayerIp = table.Column<string>(type: "TEXT", nullable: true),
                    PlayerFingerprint = table.Column<string>(type: "TEXT", nullable: true),
                    Contribution = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrismContributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrismContributions_PrismBattles_BattleId",
                        column: x => x.BattleId,
                        principalTable: "PrismBattles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FactionAreaBonuses_PrismId",
                table: "FactionAreaBonuses",
                column: "PrismId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_KillLogs_AttackerId",
                table: "Player_KillLogs",
                column: "AttackerId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_KillLogs_VictimId",
                table: "Player_KillLogs",
                column: "VictimId");

            migrationBuilder.CreateIndex(
                name: "IX_PrismBattles_PrismId",
                table: "PrismBattles",
                column: "PrismId");

            migrationBuilder.CreateIndex(
                name: "IX_PrismContributions_BattleId",
                table: "PrismContributions",
                column: "BattleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FactionAreaBonuses");

            migrationBuilder.DropTable(
                name: "Player_KillLogs");

            migrationBuilder.DropTable(
                name: "PrismContributions");

            migrationBuilder.DropTable(
                name: "PrismBattles");

            migrationBuilder.DropTable(
                name: "Prisms");

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
