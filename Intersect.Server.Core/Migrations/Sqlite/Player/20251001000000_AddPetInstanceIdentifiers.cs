using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Player
{
    /// <inheritdoc />
    public partial class AddPetInstanceIdentifiers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Player_Pets_PlayerId_PetDescriptorId",
                table: "Player_Pets");

            migrationBuilder.AddColumn<Guid?>
            (
                name: "PetInstanceId",
                table: "Player_Items",
                type: "TEXT",
                nullable: true
            );

            migrationBuilder.AddColumn<Guid?>
            (
                name: "PetInstanceId",
                table: "Player_Bank",
                type: "TEXT",
                nullable: true
            );

            migrationBuilder.AddColumn<Guid?>
            (
                name: "PetInstanceId",
                table: "Bag_Items",
                type: "TEXT",
                nullable: true
            );

            migrationBuilder.AddColumn<Guid?>
            (
                name: "PetInstanceId",
                table: "Guild_Bank",
                type: "TEXT",
                nullable: true
            );

            migrationBuilder.AddColumn<Guid>
            (
                name: "PetInstanceId",
                table: "Player_Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000")
            );

            migrationBuilder.CreateIndex(
                name: "IX_Player_Pets_PlayerId_PetInstanceId",
                table: "Player_Pets",
                columns: new[] { "PlayerId", "PetInstanceId" },
                unique: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Player_Pets_PlayerId_PetInstanceId",
                table: "Player_Pets");

            migrationBuilder.DropColumn(
                name: "PetInstanceId",
                table: "Player_Items");

            migrationBuilder.DropColumn(
                name: "PetInstanceId",
                table: "Player_Bank");

            migrationBuilder.DropColumn(
                name: "PetInstanceId",
                table: "Bag_Items");

            migrationBuilder.DropColumn(
                name: "PetInstanceId",
                table: "Guild_Bank");

            migrationBuilder.DropColumn(
                name: "PetInstanceId",
                table: "Player_Pets");

            migrationBuilder.CreateIndex(
                name: "IX_Player_Pets_PlayerId_PetDescriptorId",
                table: "Player_Pets",
                columns: new[] { "PlayerId", "PetDescriptorId" },
                unique: true
            );
        }
    }
}
