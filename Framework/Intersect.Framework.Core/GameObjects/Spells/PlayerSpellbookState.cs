using System;
using System.Collections.Generic;
using MessagePack;

namespace Intersect.Framework.Core.GameObjects.Spells;

[MessagePackObject]
public partial class PlayerSpellbookState
{
    [Key(0)]
    public int AvailableSpellPoints { get; set; }

    [Key(1)]
    public Dictionary<Guid, SpellProperties> Spells { get; set; } = [];
}

