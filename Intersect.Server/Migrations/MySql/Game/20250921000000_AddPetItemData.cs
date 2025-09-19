using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.MySql.Game
{
    /// <inheritdoc />
    public partial class AddPetItemData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Pet_BindOnEquip",
                table: "Items",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Pet_DespawnOnUnequip",
                table: "Items",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Pet_PetDescriptorId",
                table: "Items",
                type: "char(36)",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<string>(
                name: "Pet_PetNameOverride",
                table: "Items",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "Pet_SummonOnEquip",
                table: "Items",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pet_BindOnEquip",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Pet_DespawnOnUnequip",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Pet_PetDescriptorId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Pet_PetNameOverride",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Pet_SummonOnEquip",
                table: "Items");
        }
    }
}
