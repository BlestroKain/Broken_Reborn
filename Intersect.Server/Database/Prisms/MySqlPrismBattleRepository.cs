using System.Threading;
using System.Threading.Tasks;
using Intersect.Server.Database.PlayerData;
using Intersect.Server.Database.Prisms;
using Microsoft.EntityFrameworkCore;

namespace Intersect.Server.Database.Prisms;

public sealed class MySqlPrismBattleRepository : IPrismBattleRepository
{
    private readonly MySqlPlayerContext _context;

    public MySqlPrismBattleRepository(MySqlPlayerContext context)
    {
        _context = context;
    }

    public DbSet<PrismBattle> Battles => _context.PrismBattles;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}

