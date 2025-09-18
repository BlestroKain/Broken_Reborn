using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Player
{
    /// <inheritdoc />
    public partial class AddPlayerPets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ActivePetId",
                table: "Players",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Player_Pets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlayerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PetDescriptorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CustomName = table.Column<string>(type: "TEXT", nullable: true),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Experience = table.Column<long>(type: "INTEGER", nullable: false),
                    TimeCreated = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_Pets_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_ActivePetId",
                table: "Players",
                column: "ActivePetId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_Pets_PlayerId",
                table: "Player_Pets",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_Pets_PlayerId_PetDescriptorId",
                table: "Player_Pets",
                columns: new[] { "PlayerId", "PetDescriptorId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Player_Pets_ActivePetId",
                table: "Players",
                column: "ActivePetId",
                principalTable: "Player_Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Player_Pets_ActivePetId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "Player_Pets");

            migrationBuilder.DropIndex(
                name: "IX_Players_ActivePetId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ActivePetId",
                table: "Players");
        }
    }
}
