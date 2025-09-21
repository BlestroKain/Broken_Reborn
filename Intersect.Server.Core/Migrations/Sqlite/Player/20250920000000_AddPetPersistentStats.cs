using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Player
{
    /// <inheritdoc />
    public partial class AddPetPersistentStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatPoints",
                table: "Player_Pets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PetBaseStats",
                table: "Player_Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PetStatPointAllocations",
                table: "Player_Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PetMaxVitals",
                table: "Player_Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PetVitals",
                table: "Player_Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatPoints",
                table: "Player_Pets");

            migrationBuilder.DropColumn(
                name: "PetBaseStats",
                table: "Player_Pets");

            migrationBuilder.DropColumn(
                name: "PetStatPointAllocations",
                table: "Player_Pets");

            migrationBuilder.DropColumn(
                name: "PetMaxVitals",
                table: "Player_Pets");

            migrationBuilder.DropColumn(
                name: "PetVitals",
                table: "Player_Pets");
        }
    }
}
