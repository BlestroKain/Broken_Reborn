using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Player
{
    /// <inheritdoc />
    public partial class AddSpellsLevels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpellId",
                table: "Player_Spells",
                newName: "PlayerSpellId");

            migrationBuilder.AddColumn<int>(
                name: "SpellPoints",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Properties",
                table: "Player_Spells",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlayerSpell",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SpellId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Properties = table.Column<string>(type: "TEXT", nullable: true),
                    SpellPointsSpent = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerId = table.Column<Guid>(type: "TEXT", nullable: false)
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

            migrationBuilder.Sql(
                @"INSERT INTO PlayerSpell (Id, SpellId, Properties, SpellPointsSpent, PlayerId)
                  SELECT Id, PlayerSpellId, Properties, 0, PlayerId FROM Player_Spells;
                  UPDATE Player_Spells SET PlayerSpellId = Id;");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Spells_PlayerSpell_PlayerSpellId",
                table: "Player_Spells");

            migrationBuilder.Sql(
                @"UPDATE Player_Spells
                   SET PlayerSpellId = (
                       SELECT SpellId FROM PlayerSpell WHERE PlayerSpell.Id = Player_Spells.PlayerSpellId
                   );");

            migrationBuilder.DropTable(
                name: "PlayerSpell");

            migrationBuilder.DropIndex(
                name: "IX_Player_Spells_PlayerSpellId",
                table: "Player_Spells");

            migrationBuilder.DropColumn(
                name: "SpellPoints",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Properties",
                table: "Player_Spells");

            migrationBuilder.RenameColumn(
                name: "PlayerSpellId",
                table: "Player_Spells",
                newName: "SpellId");
        }
    }
}
