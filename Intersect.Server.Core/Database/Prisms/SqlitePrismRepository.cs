using System.Threading;
using System.Threading.Tasks;
using Intersect.Server.Database.PlayerData;
using Microsoft.EntityFrameworkCore;

namespace Intersect.Server.Database.Prisms;

public sealed class SqlitePrismRepository : IPrismRepository
{
    private readonly SqlitePlayerContext _context;

    public SqlitePrismRepository(SqlitePlayerContext context)
    {
        _context = context;
    }

    public DbSet<AlignmentPrism> Prisms => _context.Prisms;

    public DbSet<FactionAreaBonus> FactionAreaBonuses => _context.FactionAreaBonuses;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}

