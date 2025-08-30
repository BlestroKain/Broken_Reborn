using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Intersect.Server.Database.Prisms;

public interface IPrismContributionRepository
{
    DbSet<PrismContribution> Contributions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

