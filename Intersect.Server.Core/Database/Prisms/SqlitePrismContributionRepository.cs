using System.Threading;
using System.Threading.Tasks;
using Intersect.Server.Database.PlayerData;
using Microsoft.EntityFrameworkCore;

namespace Intersect.Server.Database.Prisms;

public sealed class SqlitePrismContributionRepository : IPrismContributionRepository
{
    private readonly SqlitePlayerContext _context;

    public SqlitePrismContributionRepository(SqlitePlayerContext context)
    {
        _context = context;
    }

    public DbSet<PrismContribution> Contributions => _context.PrismContributions;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}

