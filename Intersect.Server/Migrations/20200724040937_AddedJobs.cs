using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class PrincipalSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
              name: "FarmingExp",
              table: "Players",
              nullable: false,
              defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "FarmingLevel",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "FishingExp",
                table: "Players",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "FishingLevel",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "MiningExp",
                table: "Players",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "MiningLevel",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "WoodExp",
                table: "Players",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "WoodLevel",
                table: "Players",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<long>(
                name: "HuntingExp",
                table: "Players",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "HuntingLevel",
                table: "Players",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<long>(
                name: "BlacksmithExp",
                table: "Players",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "BlacksmithLevel",
                table: "Players",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<long>(
                name: "CookingExp",
                table: "Players",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "CookingLevel",
                table: "Players",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<long>(
                name: "AlchemyExp",
                table: "Players",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "AlchemyLevel",
                table: "Players",
                nullable: false,
                defaultValue: 0);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FarmingExp",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "FarmingLevel",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "FishingExp",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "FishingLevel",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "MiningExp",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "MiningLevel",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "WoodExp",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "WoodLevel",
                table: "Players");
            migrationBuilder.DropColumn(
                name: "HuntingExp",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "HuntingLevel",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "BlacksmithExp",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "BlacksmithLevel",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CookingExp",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CookingLevel",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "AlchemyExp",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "AlchemyLevel",
                table: "Players");
        }
    }
}
