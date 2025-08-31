using System;
using System.Collections.Generic;
using Intersect.Framework.Core.GameObjects.Prisms;
using Intersect.Server.Database.Prisms;

namespace Intersect.Server.Services.Prisms;

/// <summary>
///     Runtime representation of a prism. Combines static descriptor
///     information with mutable entity state and exposes convenience
///     properties for common accessors.
/// </summary>
public class PrismRuntime
{
    public PrismRuntime(PrismDescriptor descriptor, PrismEntity entity)
    {
        Descriptor = descriptor;
        Entity = entity;
    }

    public PrismDescriptor Descriptor { get; }
    public PrismEntity Entity { get; }

    public Guid Id => Descriptor.Id;
    public Guid MapId => Descriptor.MapId;
    public (int X, int Y) Pos => (Descriptor.X, Descriptor.Y);

    public int Owner
    {
        get => Entity.Owner;
        set => Entity.Owner = value;
    }

    public PrismState State
    {
        get => Entity.State;
        set => Entity.State = value;
    }

    public int Hp
    {
        get => Entity.Hp;
        set => Entity.Hp = value;
    }

    public int MaxHp
    {
        get => Entity.MaxHp;
        set => Entity.MaxHp = value;
    }

    public DateTime? LastHitAt
    {
        get => Entity.LastHitAt;
        set => Entity.LastHitAt = value;
    }

    public DateTime? LastStateChangeAt
    {
        get => Entity.LastStateChangeAt;
        set => Entity.LastStateChangeAt = value;
    }

    public Guid? CurrentBattleId
    {
        get => Entity.CurrentBattleId;
        set => Entity.CurrentBattleId = value;
    }

    public List<VulnerabilityWindow> Windows => Descriptor.Windows;
    public PrismArea Area => Descriptor.Area;
    public List<PrismModule> Modules => Descriptor.Modules;
}
