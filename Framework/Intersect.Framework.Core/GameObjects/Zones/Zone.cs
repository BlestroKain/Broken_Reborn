using System;
using Intersect.GameObjects;
using Intersect.Models;
using Newtonsoft.Json;

namespace Intersect.Framework.Core.GameObjects.Zones;

public partial class Zone : DatabaseObject<Zone>
{
    public Zone()
    {
    }

    [JsonConstructor]
    public Zone(Guid id) : base(id)
    {
    }

    public string Name { get; set; } = string.Empty;

    public ZoneFlags Flags { get; set; } = ZoneFlags.None;

    public ZoneModifiers Modifiers { get; set; } = new ZoneModifiers();
}

