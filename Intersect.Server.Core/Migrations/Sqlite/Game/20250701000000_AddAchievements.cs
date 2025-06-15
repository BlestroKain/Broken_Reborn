using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Game;

public partial class AddAchievements : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Achievements",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                TimeCreated = table.Column<long>(type: "INTEGER", nullable: false),
                Name = table.Column<string>(type: "TEXT", nullable: true),
                Category = table.Column<string>(type: "TEXT", nullable: true),
                Difficulty = table.Column<int>(type: "INTEGER", nullable: false),
                Requirements = table.Column<string>(type: "TEXT", nullable: true),
                Rewards = table.Column<string>(type: "TEXT", nullable: true),
                Folder = table.Column<string>(type: "TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Achievements", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Achievements");
    }
}
