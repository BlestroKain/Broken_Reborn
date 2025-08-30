using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Intersect.Server.Entities;

namespace Intersect.Server.Database.PlayerData;

public interface IPlayerRepository
{
    DbSet<Player> Players { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

