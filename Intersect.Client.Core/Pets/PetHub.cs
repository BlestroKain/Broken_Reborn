using System;
using System.Collections.Generic;
using Intersect.Client.Entities;
using Intersect.Client.General;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Pets;
using Intersect.Network.Packets.Client;
using Intersect.Network.Packets.Server;

namespace Intersect.Client.Core.Pets;

public sealed class PetHub
{
    private readonly object _syncRoot = new();
    private Pet? _activePet;
    private PetState _behavior = PetState.Follow;
    private PetState? _pendingBehavior;
    private PetDescriptor? _equippedDescriptor;
    private string? _equippedPetName;
    private bool _isSpawnRequested;

    public PetHub()
    {
        Globals.PetMetadataChanged += OnPetMetadataChanged;
    }

    public event Action? ActivePetChanged;

    public event Action? BehaviorChanged;

    public event Action? SpawnStateChanged;

    public Pet? ActivePet
    {
        get
        {
            lock (_syncRoot)
            {
                return _activePet is { IsDisposed: false } ? _activePet : null;
            }
        }
    }

    public PetState Behavior
    {
        get
        {
            lock (_syncRoot)
            {
                return _behavior;
            }
        }
    }

    public bool HasActivePet
    {
        get
        {
            lock (_syncRoot)
            {
                return (_activePet is { IsDisposed: false }) || _equippedDescriptor != null;
            }
        }
    }

    public PetDescriptor? EquippedDescriptor
    {
        get
        {
            lock (_syncRoot)
            {
                return _equippedDescriptor;
            }
        }
    }

    public string? EquippedPetName
    {
        get
        {
            lock (_syncRoot)
            {
                return _equippedPetName;
            }
        }
    }

    public bool IsSpawnRequested
    {
        get
        {
            lock (_syncRoot)
            {
                return _isSpawnRequested;
            }
        }
    }

    public bool InvokePet(bool openPetHub = true)
    {
        lock (_syncRoot)
        {
            if (_isSpawnRequested)
            {
                return false;
            }

            if (_activePet is not { IsDisposed: false } && _equippedDescriptor == null)
            {
                return false;
            }
        }

        Intersect.Client.Networking. Network.SendPacket(new SpawnPetRequestPacket(openPetHub));
        return true;
    }

    public bool DismissPet(bool closePetHub = false)
    {
        lock (_syncRoot)
        {
            if (!_isSpawnRequested)
            {
                return false;
            }
        }

        Intersect.Client.Networking. Network.SendPacket(new DespawnPetRequestPacket(closePetHub));
        return true;
    }

    public void HandlePetLeft(Guid petId)
    {
        bool activeChanged;
        bool behaviorChanged;

        lock (_syncRoot)
        {
            if (_activePet?.Id != petId)
            {
                return;
            }

            (activeChanged, behaviorChanged) = UpdateState(null, PetState.Follow, true);
        }

        if (behaviorChanged)
        {
            BehaviorChanged?.Invoke();
        }

        if (activeChanged)
        {
            ActivePetChanged?.Invoke();
        }
    }

    public void Process(PetEntityPacket packet)
    {
        if (packet == null)
        {
            return;
        }

        bool activeChanged = false;
        bool behaviorChanged = false;

        lock (_syncRoot)
        {
            if (!Globals.TryGetEntity(EntityType.Pet, packet.EntityId, out var entity) || entity is not Pet pet)
            {
                if (_activePet?.Id == packet.EntityId)
                {
                    (activeChanged, behaviorChanged) = UpdateState(null, PetState.Follow, true);
                }

                goto RaiseEvents;
            }

            if (!pet.IsOwnedByLocalPlayer)
            {
                if (_activePet?.Id == pet.Id)
                {
                    (activeChanged, behaviorChanged) = UpdateState(null, PetState.Follow, true);
                }

                goto RaiseEvents;
            }

            (activeChanged, behaviorChanged) = UpdateState(pet, packet.Behavior, true);
        }

RaiseEvents:
        if (behaviorChanged)
        {
            BehaviorChanged?.Invoke();
        }

        if (activeChanged)
        {
            ActivePetChanged?.Invoke();
        }
    }

    public void Process(PetHubStatePacket packet)
    {
        if (packet == null)
        {
            return;
        }

        var spawnChanged = false;

        lock (_syncRoot)
        {
            if (_isSpawnRequested == packet.IsActive)
            {
                return;
            }

            _isSpawnRequested = packet.IsActive;
            spawnChanged = true;
        }

        if (spawnChanged)
        {
            SpawnStateChanged?.Invoke();
        }
    }

    public void Process(PetStateUpdatePacket packet)
    {
        if (packet == null)
        {
            return;
        }

        bool activeChanged = false;
        bool behaviorChanged = false;

        lock (_syncRoot)
        {
            if (!Globals.TryGetEntity(EntityType.Pet, packet.PetId, out var entity) || entity is not Pet pet)
            {
                if (_activePet?.Id == packet.PetId)
                {
                    (activeChanged, behaviorChanged) = UpdateState(null, PetState.Follow, true);
                }

                goto RaiseEvents;
            }

            if (!pet.IsOwnedByLocalPlayer)
            {
                if (_activePet?.Id == pet.Id)
                {
                    (activeChanged, behaviorChanged) = UpdateState(null, PetState.Follow, true);
                }

                goto RaiseEvents;
            }

            (activeChanged, behaviorChanged) = UpdateState(pet, packet.Behavior, true);
        }

RaiseEvents:
        if (behaviorChanged)
        {
            BehaviorChanged?.Invoke();
        }

        if (activeChanged)
        {
            ActivePetChanged?.Invoke();
        }
    }

    public void Reset()
    {
        bool activeChanged;
        bool behaviorChanged;
        bool spawnChanged;
        bool descriptorChanged;

        lock (_syncRoot)
        {
            activeChanged = _activePet != null;
            _activePet = null;
            _pendingBehavior = null;
            behaviorChanged = _behavior != PetState.Follow;
            _behavior = PetState.Follow;
            spawnChanged = _isSpawnRequested;
            _isSpawnRequested = false;
            descriptorChanged = _equippedDescriptor != null || !string.IsNullOrWhiteSpace(_equippedPetName);
            _equippedDescriptor = null;
            _equippedPetName = null;
        }

        if (behaviorChanged)
        {
            BehaviorChanged?.Invoke();
        }

        if (activeChanged || descriptorChanged)
        {
            ActivePetChanged?.Invoke();
        }

        if (spawnChanged)
        {
            SpawnStateChanged?.Invoke();
        }
    }

    public bool SetBehavior(PetState behavior)
    {
        Pet? pet;
        Guid? petToClear = null;
        bool shouldSend;

        lock (_syncRoot)
        {
            pet = _activePet;
            if (pet == null)
            {
                return false;
            }

            if (!IsSelectableBehavior(behavior))
            {
                return false;
            }

            if (pet.IsDisposed)
            {
                petToClear = pet.Id;
                shouldSend = false;
            }
            else
            {
                shouldSend = _behavior != behavior || _pendingBehavior != behavior;
                _pendingBehavior = behavior;
            }
        }

        if (petToClear.HasValue)
        {
            HandlePetLeft(petToClear.Value);
            return false;
        }

        if (!shouldSend || pet == null)
        {
            return false;
        }

        Intersect.Client.Networking.Network.SendPacket(new PetBehaviorChangePacket(behavior, pet.Id));
        return true;
    }

    private void OnPetMetadataChanged(Pet pet)
    {
        bool activeChanged = false;
        bool behaviorChanged = false;

        lock (_syncRoot)
        {
            if (pet.IsOwnedByLocalPlayer)
            {
                (activeChanged, behaviorChanged) = UpdateState(pet, pet.Behavior, true);
            }
            else if (_activePet?.Id == pet.Id)
            {
                (activeChanged, behaviorChanged) = UpdateState(null, PetState.Follow, true);
            }
        }

        if (behaviorChanged)
        {
            BehaviorChanged?.Invoke();
        }

        if (activeChanged)
        {
            ActivePetChanged?.Invoke();
        }
    }

    public void SyncEquippedPet(Player? player)
    {
        PetDescriptor? descriptor = null;
        string? petName = null;

        if (player?.Inventory != null)
        {
            foreach (var slotIndices in player.MyEquipment.Values)
            {
                if (slotIndices == null)
                {
                    continue;
                }

                foreach (var slotIndex in slotIndices)
                {
                    if (slotIndex < 0 || slotIndex >= player.Inventory.Length)
                    {
                        continue;
                    }

                    var item = player.Inventory[slotIndex];
                    var petData = item?.Descriptor?.Pet;
                    if (petData?.PetDescriptorId == Guid.Empty)
                    {
                        continue;
                    }

                    descriptor = petData.Descriptor;
                    if (descriptor == null)
                    {
                        continue;
                    }

                    petName = string.IsNullOrWhiteSpace(petData.PetNameOverride)
                        ? null
                        : petData.PetNameOverride;

                    goto FoundDescriptor;
                }
            }
        }

FoundDescriptor:
        UpdateEquippedPet(descriptor, petName);
    }

    private static bool IsSelectableBehavior(PetState behavior) =>
        behavior is PetState.Follow or PetState.Stay or PetState.Defend or PetState.Passive;

    private (bool activeChanged, bool behaviorChanged) UpdateState(Pet? pet, PetState behavior, bool clearPending)
    {
        if (!IsSelectableBehavior(behavior))
        {
            behavior = PetState.Follow;
        }

        var activeChanged = false;
        var behaviorChanged = false;

        if (pet != null)
        {
            _equippedDescriptor = pet.Descriptor ?? _equippedDescriptor;
            _equippedPetName = string.IsNullOrWhiteSpace(pet.Name) ? _equippedPetName : pet.Name;
        }

        if (!ReferenceEquals(_activePet, pet))
        {
            _activePet = pet;
            activeChanged = true;
        }

        if (clearPending)
        {
            _pendingBehavior = null;
        }

        if (_behavior != behavior)
        {
            _behavior = behavior;
            behaviorChanged = true;
        }

        return (activeChanged, behaviorChanged);
    }

    private void UpdateEquippedPet(PetDescriptor? descriptor, string? petName)
    {
        bool descriptorChanged;
        lock (_syncRoot)
        {
            petName = string.IsNullOrWhiteSpace(petName) ? null : petName;
            descriptorChanged = !ReferenceEquals(_equippedDescriptor, descriptor)
                || !string.Equals(_equippedPetName, petName, StringComparison.Ordinal);

            if (!descriptorChanged)
            {
                return;
            }

            _equippedDescriptor = descriptor;
            _equippedPetName = petName;
        }

        ActivePetChanged?.Invoke();
    }
}
