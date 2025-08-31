using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Framework.Core.GameObjects.Prisms;

namespace Intersect.Server.Database.Prisms;

public partial class AlignmentPrism
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Map identifier where the prism is located.
    /// </summary>
    public Guid MapId { get; set; }

    /// <summary>
    /// Faction currently owning the prism.
    /// </summary>
    public int Faction { get; set; }

    /// <summary>
    /// Current state of the prism.
    /// </summary>
    public PrismState State { get; set; }

    /// <summary>
    /// X-coordinate where the prism resides on the map.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Y-coordinate where the prism resides on the map.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Date and time when the prism was placed.
    /// </summary>
    public DateTime PlacedAt { get; set; }

    /// <summary>
    /// Time when the maturation period ends and the prism becomes vulnerable.
    /// </summary>
    public DateTime? MaturationEndsAt { get; set; }

    /// <summary>
    /// Current level of the prism.
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// Current hit points of the prism.
    /// </summary>
    public int Hp { get; set; }

    /// <summary>
    /// Time when the prism was last hit.
    /// </summary>
    public DateTime? LastHitAt { get; set; }

    /// <summary>
    /// Maximum hit points of the prism.
    /// </summary>
    public int MaxHp { get; set; }

    /// <summary>
    /// Identifier of the current battle if the prism is under attack.
    /// </summary>
    public Guid? CurrentBattleId { get; set; }

    /// <summary>
    /// Vulnerability windows in which the prism can be attacked.
    /// </summary>
    public List<VulnerabilityWindow> Windows { get; set; } = new();

    /// <summary>
    /// Installed modules affecting the prism.
    /// </summary>
    public List<PrismModule> Modules { get; set; } = new();

    /// <summary>
    /// Area influenced by the prism.
    /// </summary>
    public PrismArea Area { get; set; } = new();

    /// <summary>
    /// Animation to display while the prism is idle.
    /// </summary>
    public Guid? IdleAnimationId { get; set; } = Guid.Empty;

    /// <summary>
    /// Animation to display while the prism is vulnerable.
    /// </summary>
    public Guid? VulnerableAnimationId { get; set; } = Guid.Empty;

    /// <summary>
    /// Animation to display when the prism is under attack.
    /// </summary>
    public Guid? UnderAttackAnimationId { get; set; } = Guid.Empty;

    /// <summary>
    /// Indicates whether the prism sprite should be tinted by the owning faction.
    /// </summary>
    public bool TintByFaction { get; set; } = false;

    /// <summary>
    /// Vertical offset applied to the prism sprite.
    /// </summary>
    public int SpriteOffsetY { get; set; } = 0;
}

