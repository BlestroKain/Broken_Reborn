using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.MySql.Player
{
    public partial class AddPlayerPets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ActivePetId",
                table: "Players",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Player_Pets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false),
                    PetDescriptorId = table.Column<Guid>(nullable: false),
                    CustomName = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Experience = table.Column<long>(nullable: false),
                    TimeCreated = table.Column<long>(nullable: false)
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
