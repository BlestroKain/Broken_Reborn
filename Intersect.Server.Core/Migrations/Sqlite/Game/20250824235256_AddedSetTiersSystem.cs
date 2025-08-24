using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Game
{
    /// <inheritdoc />
    public partial class AddedSetTiersSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Sets_Set",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Set",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "VitalsRegen",
                table: "Sets",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldDefaultValue: "[]");

            migrationBuilder.AlterColumn<string>(
                name: "VitalsGiven",
                table: "Sets",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldDefaultValue: "[]");

            migrationBuilder.AlterColumn<string>(
                name: "StatsGiven",
                table: "Sets",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldDefaultValue: "[]");

            migrationBuilder.AlterColumn<string>(
                name: "PercentageVitalsGiven",
                table: "Sets",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldDefaultValue: "[]");

            migrationBuilder.AlterColumn<string>(
                name: "PercentageStatsGiven",
                table: "Sets",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldDefaultValue: "[]");

            migrationBuilder.AlterColumn<string>(
                name: "ItemIds",
                table: "Sets",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldDefaultValue: "[]");

            migrationBuilder.AlterColumn<string>(
                name: "Effects",
                table: "Sets",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldDefaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "BonusTiers",
                table: "Sets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Effects",
                table: "Items",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldDefaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BonusTiers",
                table: "Sets");

            migrationBuilder.AlterColumn<string>(
                name: "VitalsRegen",
                table: "Sets",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VitalsGiven",
                table: "Sets",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StatsGiven",
                table: "Sets",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PercentageVitalsGiven",
                table: "Sets",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PercentageStatsGiven",
                table: "Sets",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemIds",
                table: "Sets",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Effects",
                table: "Sets",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Effects",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_Set",
                table: "Items",
                column: "Set");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Sets_Set",
                table: "Items",
                column: "Set",
                principalTable: "Sets",
                principalColumn: "Id");
        }
    }
}
