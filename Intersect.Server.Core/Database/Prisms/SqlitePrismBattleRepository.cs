using System.Threading;
using System.Threading.Tasks;
using Intersect.Server.Database.PlayerData;
using Microsoft.EntityFrameworkCore;

namespace Intersect.Server.Database.Prisms;

public sealed class SqlitePrismBattleRepository : IPrismBattleRepository
{
    private readonly SqlitePlayerContext _context;

    public SqlitePrismBattleRepository(SqlitePlayerContext context)
    {
        _context = context;
    }

    public DbSet<PrismBattle> Battles => _context.PrismBattles;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}

