using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.MySql.Game;

public partial class AddAchievements : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Achievements",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false)
                    .Annotation("MySql:Collation", "ascii_general_ci"),
                TimeCreated = table.Column<long>(type: "bigint", nullable: false),
                Name = table.Column<string>(type: "longtext", nullable: true),
                Category = table.Column<string>(type: "longtext", nullable: true),
                Difficulty = table.Column<int>(type: "int", nullable: false),
                Requirements = table.Column<string>(type: "longtext", nullable: true),
                Rewards = table.Column<string>(type: "longtext", nullable: true),
                Folder = table.Column<string>(type: "longtext", nullable: true)
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
