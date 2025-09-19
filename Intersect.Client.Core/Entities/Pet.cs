using System;
using Intersect.Client.General;
using Intersect.Enums;
using Intersect.Framework.Core;
using Intersect.Framework.Core.GameObjects.Pets;
using Intersect.Network.Packets.Server;

namespace Intersect.Client.Entities;

/// <summary>
///     Represents a pet entity on the client. This entity mirrors most of the behaviour of other global
///     entities while providing additional metadata required to relate it to its owner and descriptor.
/// </summary>
public sealed class Pet : Entity
{
    private PetDescriptor? _cachedDescriptor;
    private Guid _descriptorId;

    public Pet(Guid id, EntityPacket? packet)
        : base(id, packet, EntityType.Pet)
    {
        mRenderPriority = 2;
    }

    /// <summary>
    ///     Gets the identifier of the player that owns this pet.
    /// </summary>
    public Guid OwnerId { get; private set; }

    /// <summary>
    ///     Gets a value indicating whether the server considers this pet despawnable.
    /// </summary>
    public bool Despawnable { get; private set; }

    /// <summary>
    ///     Gets the current state reported by the server for the pet.
    /// </summary>
    public PetState State { get; private set; } = PetState.Idle;

    /// <summary>
    ///     Gets the descriptor identifier used to spawn this pet.
    /// </summary>
    public Guid DescriptorId
    {
        get => _descriptorId;
        private set
        {
            if (_descriptorId == value)
            {
                return;
            }

            _descriptorId = value;
            _cachedDescriptor = null;
        }
    }

    /// <summary>
    ///     Gets the descriptor associated with the pet, if it is available in the local cache.
    /// </summary>
    public PetDescriptor? Descriptor
    {
        get
        {
            if (_cachedDescriptor == null && DescriptorId != Guid.Empty)
            {
                PetDescriptor.Lookup.TryGetValue(DescriptorId, out _cachedDescriptor);
            }

            return _cachedDescriptor;
        }
    }

    /// <summary>
    ///     Gets the player that owns this pet if the player entity is currently known by the client.
    /// </summary>
    public Player? Owner =>
        OwnerId != Guid.Empty && Globals.TryGetEntity(EntityType.Player, OwnerId, out var entity)
            ? entity as Player
            : null;

    /// <summary>
    ///     Returns <c>true</c> when the supplied player is the pet owner.
    /// </summary>
    public bool IsOwner(Player? player) => player?.Id == OwnerId;

    /// <summary>
    ///     Returns <c>true</c> when the local player owns this pet.
    /// </summary>
    public bool IsOwnedByLocalPlayer => IsOwner(Globals.Me);

    /// <summary>
    ///     Applies the metadata provided by the server to this pet instance.
    /// </summary>
    /// <param name="ownerId">Identifier of the player that owns the pet.</param>
    /// <param name="descriptorId">Identifier of the descriptor that spawned the pet.</param>
    /// <param name="state">Current state reported by the server.</param>
    /// <param name="despawnable">Indicates whether the pet can despawn automatically.</param>
    public void ApplyMetadata(Guid ownerId, Guid descriptorId, PetState state, bool despawnable)
    {
        OwnerId = ownerId;
        DescriptorId = descriptorId;
        State = state;
        Despawnable = despawnable;
    }

    /// <inheritdoc />
    public override void Dispose()
    {
        base.Dispose();

        _cachedDescriptor = null;
        OwnerId = Guid.Empty;
        DescriptorId = Guid.Empty;
        State = PetState.Idle;
        Despawnable = false;
    }

    /// <inheritdoc />
    public override void Load(EntityPacket? packet)
    {
        base.Load(packet);

        if (packet is not PetEntityPacket petPacket)
        {
            return;
        }

        ApplyMetadata(petPacket.OwnerId, petPacket.DescriptorId, petPacket.State, petPacket.Despawnable);
    }
}
