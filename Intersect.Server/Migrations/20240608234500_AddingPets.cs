using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class AddingPets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TimeCreated = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Immunities = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Experience = table.Column<long>(nullable: false),
                    Aggressive = table.Column<bool>(nullable: false),
                    Movement = table.Column<byte>(nullable: false),
                    AttackAnimation = table.Column<Guid>(nullable: false),
                    DeathAnimation = table.Column<Guid>(nullable: false),
                    Damage = table.Column<int>(nullable: false),
                    DamageType = table.Column<int>(nullable: false),
                    CritChance = table.Column<int>(nullable: false),
                    CritMultiplier = table.Column<double>(nullable: false),
                    Spells = table.Column<string>(nullable: true),
                    Sprite = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    MaxVital = table.Column<string>(nullable: true),
                    BaseStats = table.Column<string>(nullable: true),
                    VitalRegen = table.Column<string>(nullable: true),
                    Folder = table.Column<string>(nullable: true),
                    Scaling = table.Column<decimal>(nullable: false),
                    ScalingStat = table.Column<int>(nullable: false),
                    AttackSpeedModifier = table.Column<int>(nullable: false),
                    AttackSpeedValue = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pets");
            
        }
    }
}
