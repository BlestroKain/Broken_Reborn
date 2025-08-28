using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Intersect.Server.Database.Prisms;

public interface IPrismRepository
{
    DbSet<AlignmentPrism> Prisms { get; }
    DbSet<FactionAreaBonus> FactionAreaBonuses { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

