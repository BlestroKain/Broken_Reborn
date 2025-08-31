using System;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Framework.Core.GameObjects.Prisms;

namespace Intersect.Server.Database.Prisms;

/// <summary>
///     Persistent state for a prism in the world. This entity only stores
///     the mutable runtime information while static data lives in
///     <see cref="PrismDescriptor"/>.
/// </summary>
public class PrismEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid PrismId { get; set; }

    /// <summary>
    ///     Faction that currently owns the prism.
    /// </summary>
    public int Owner { get; set; }

    /// <summary>
    ///     Current state of the prism.
    /// </summary>
    public PrismState State { get; set; }

    /// <summary>
    ///     Current hit points.
    /// </summary>
    public int Hp { get; set; }

    /// <summary>
    ///     Maximum hit points.
    /// </summary>
    public int MaxHp { get; set; }

    /// <summary>
    ///     Time when the prism was last damaged.
    /// </summary>
    public DateTime? LastHitAt { get; set; }

    /// <summary>
    ///     Time when the prism state last changed.
    /// </summary>
    public DateTime? LastStateChangeAt { get; set; }

    /// <summary>
    ///     Identifier of the current battle if the prism is under attack.
    /// </summary>
    public Guid? CurrentBattleId { get; set; }
}

