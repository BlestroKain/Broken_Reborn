using System.Threading;
using System.Threading.Tasks;
using Intersect.Server.Database.PlayerData;
using Intersect.Server.Database.Prisms;
using Microsoft.EntityFrameworkCore;

namespace Intersect.Server.Database.Prisms;

public sealed class MySqlPrismContributionRepository : IPrismContributionRepository
{
    private readonly MySqlPlayerContext _context;

    public MySqlPrismContributionRepository(MySqlPlayerContext context)
    {
        _context = context;
    }

    public DbSet<PrismContribution> Contributions => _context.PrismContributions;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}

