using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intersect.Server.Migrations.MySql.Player;

public partial class AddAchievements : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Player_Achievements",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false)
                    .Annotation("MySql:Collation", "ascii_general_ci"),
                PlayerId = table.Column<Guid>(type: "char(36)", nullable: false)
                    .Annotation("MySql:Collation", "ascii_general_ci"),
                AchievementId = table.Column<Guid>(type: "char(36)", nullable: false)
                    .Annotation("MySql:Collation", "ascii_general_ci"),
                Progress = table.Column<int>(type: "int", nullable: false),
                Completed = table.Column<bool>(type: "tinyint(1)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Player_Achievements", x => x.Id);
                table.ForeignKey(
                    name: "FK_Player_Achievements_Players_PlayerId",
                    column: x => x.PlayerId,
                    principalTable: "Players",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Player_Achievements_PlayerId",
            table: "Player_Achievements",
            column: "PlayerId");

        migrationBuilder.CreateIndex(
            name: "IX_Player_Achievements_AchievementId_PlayerId",
            table: "Player_Achievements",
            columns: new[] { "AchievementId", "PlayerId" },
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Player_Achievements");
    }
}
