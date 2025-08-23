using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Player
{
    /// <inheritdoc />
    public partial class AddSpellsLevelse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Spells_PlayerSpell_PlayerSpellId",
                table: "Player_Spells");

            migrationBuilder.DropIndex(
                name: "IX_Player_Spells_PlayerSpellId",
                table: "Player_Spells");

            migrationBuilder.RenameColumn(
                name: "PlayerSpellId",
                table: "Player_Spells",
                newName: "SpellId");

            migrationBuilder.AddColumn<int>(
                name: "SpellPointsSpent",
                table: "Player_Spells",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpellPointsSpent",
                table: "Player_Spells");

            migrationBuilder.RenameColumn(
                name: "SpellId",
                table: "Player_Spells",
                newName: "PlayerSpellId");

            migrationBuilder.CreateTable(
                name: "PlayerSpell",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "BLOB", nullable: false),
                    PlayerId = table.Column<Guid>(type: "BLOB", nullable: false),
                    SpellId = table.Column<Guid>(type: "BLOB", nullable: false),
                    SpellPointsSpent = table.Column<int>(type: "INTEGER", nullable: false),
                    Properties = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSpell", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerSpell_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_Spells_PlayerSpellId",
                table: "Player_Spells",
                column: "PlayerSpellId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSpell_PlayerId",
                table: "PlayerSpell",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Spells_PlayerSpell_PlayerSpellId",
                table: "Player_Spells",
                column: "PlayerSpellId",
                principalTable: "PlayerSpell",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
