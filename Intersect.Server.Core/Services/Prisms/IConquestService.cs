using System.Threading;
using System.Threading.Tasks;
using Intersect.Framework.Core.GameObjects.Prisms;
using Intersect.Server.Entities;
using Intersect.Server.Maps;

namespace Intersect.Server.Services.Prisms;

public interface IConquestService
{
    Task CaptureAsync(MapInstance map, Player captor, CancellationToken cancellationToken = default);
    Task DefendAsync(MapInstance map, CancellationToken cancellationToken = default);
    Task DestroyAsync(MapInstance map, Player destroyer, CancellationToken cancellationToken = default);
}

