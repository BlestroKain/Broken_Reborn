using Intersect.Server.Database.PlayerData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Sqlite.Player
{
    [DbContext(typeof(SqlitePlayerContext))]
    [Migration("20250822000000_AddSpellSlotLevel")]
    partial class AddSpellSlotLevel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            new SqlitePlayerContextModelSnapshot().BuildModel(modelBuilder);
        }
    }
}
