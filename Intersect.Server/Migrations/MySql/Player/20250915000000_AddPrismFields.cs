using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.MySql.Player
{
    public partial class AddPrismFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "X",
                table: "Prisms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Y",
                table: "Prisms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Prisms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Hp",
                table: "Prisms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxHp",
                table: "Prisms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Windows",
                table: "Prisms",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modules",
                table: "Prisms",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "Prisms",
                type: "longtext",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "X",
                table: "Prisms");

            migrationBuilder.DropColumn(
                name: "Y",
                table: "Prisms");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Prisms");

            migrationBuilder.DropColumn(
                name: "Hp",
                table: "Prisms");

            migrationBuilder.DropColumn(
                name: "MaxHp",
                table: "Prisms");

            migrationBuilder.DropColumn(
                name: "Windows",
                table: "Prisms");

            migrationBuilder.DropColumn(
                name: "Modules",
                table: "Prisms");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Prisms");
        }
    }
}
