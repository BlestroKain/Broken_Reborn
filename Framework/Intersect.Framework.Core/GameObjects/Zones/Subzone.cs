using System;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.GameObjects;
using Intersect.Models;
using Newtonsoft.Json;

namespace Intersect.Framework.Core.GameObjects.Zones;

public partial class Subzone : DatabaseObject<Subzone>
{
    public Subzone()
    {
    }

    [JsonConstructor]
    public Subzone(Guid id) : base(id)
    {
    }

    public string Name { get; set; } = string.Empty;

    [Column("ZoneId")]
    [JsonProperty]
    public Guid ZoneId { get; set; }

    public ZoneFlags Flags { get; set; } = ZoneFlags.None;

    public ZoneModifiers Modifiers { get; set; } = ZoneModifiers.None;
}

