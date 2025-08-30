using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Intersect.Server.Database.Prisms;

public interface IPrismBattleRepository
{
    DbSet<PrismBattle> Battles { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

