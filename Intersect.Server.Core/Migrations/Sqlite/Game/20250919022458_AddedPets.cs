using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Game
{
    /// <inheritdoc />
    public partial class AddedPets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EquipmentScaling = table.Column<string>(type: "TEXT", nullable: true),
                    Immunities = table.Column<string>(type: "TEXT", nullable: true),
                    AttackAnimation = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeathAnimation = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdleAnimation = table.Column<Guid>(type: "TEXT", nullable: false),
                    AttackSpeedModifier = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackSpeedValue = table.Column<int>(type: "INTEGER", nullable: false),
                    Damage = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageType = table.Column<int>(type: "INTEGER", nullable: false),
                    CritChance = table.Column<int>(type: "INTEGER", nullable: false),
                    CritMultiplier = table.Column<double>(type: "REAL", nullable: false),
                    Tenacity = table.Column<double>(type: "REAL", nullable: false),
                    Scaling = table.Column<int>(type: "INTEGER", nullable: false),
                    ScalingStat = table.Column<int>(type: "INTEGER", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Experience = table.Column<long>(type: "INTEGER", nullable: false),
                    Sprite = table.Column<string>(type: "TEXT", nullable: true),
                    Spells = table.Column<string>(type: "TEXT", nullable: true),
                    Stats = table.Column<string>(type: "TEXT", nullable: true),
                    MaxVital = table.Column<string>(type: "TEXT", nullable: true),
                    VitalRegen = table.Column<string>(type: "TEXT", nullable: true),
                    Folder = table.Column<string>(type: "TEXT", nullable: true),
                    TimeCreated = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pets");
        }
    }
}
