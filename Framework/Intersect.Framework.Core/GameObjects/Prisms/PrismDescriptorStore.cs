using System;
using System.Collections.Generic;

namespace Intersect.Framework.Core.GameObjects.Prisms;

public static class PrismDescriptorStore
{
    private static readonly Dictionary<Guid, PrismDescriptor> Lookup = new();

    public static void Load(IEnumerable<PrismDescriptor> descriptors)
    {
        Lookup.Clear();
        foreach (var descriptor in descriptors)
        {
            Lookup[descriptor.Id] = descriptor;
        }
    }

    public static PrismDescriptor? Get(Guid id)
    {
        return Lookup.TryGetValue(id, out var descriptor) ? descriptor : null;
    }
}
