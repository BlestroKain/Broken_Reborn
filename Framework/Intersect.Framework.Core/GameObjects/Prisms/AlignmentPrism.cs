using System;
using System.Collections.Generic;
using Intersect.Enums;

namespace Intersect.Framework.Core.GameObjects.Prisms;

public class AlignmentPrism
{
    public Guid Id { get; set; }

    public Factions Owner { get; set; }

    public PrismState State { get; set; }

    public Guid MapId { get; set; }

    public int X { get; set; }

    public int Y { get; set; }

    public int Level { get; set; }

    public int MaxHp { get; set; }

    public int Hp { get; set; }

    public DateTime PlacedAt { get; set; }

    public DateTime? MaturationEndsAt { get; set; }

    public DateTime? LastHitAt { get; set; }

    public List<VulnerabilityWindow> Windows { get; set; } = new();

    public List<PrismModule> Modules { get; set; } = new();

    public PrismArea Area { get; set; } = new();

    public Guid? IdleAnimationId { get; set; } = Guid.Empty;

    public Guid? VulnerableAnimationId { get; set; } = Guid.Empty;

    public Guid? UnderAttackAnimationId { get; set; } = Guid.Empty;

    public bool TintByFaction { get; set; } = false;

    public int SpriteOffsetY { get; set; } = 0;

    public Guid? CurrentBattleId { get; set; }
}

