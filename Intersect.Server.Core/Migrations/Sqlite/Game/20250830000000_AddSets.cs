using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Sqlite.Game
{
    public partial class AddSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TimeCreated = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Folder = table.Column<string>(nullable: true),
                    ItemIds = table.Column<string>(nullable: true),
                    StatsGiven = table.Column<string>(nullable: true),
                    PercentageStatsGiven = table.Column<string>(nullable: true),
                    VitalsGiven = table.Column<string>(nullable: true),
                    PercentageVitalsGiven = table.Column<string>(nullable: true),
                    Effects = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Sets");
        }
    }
}
