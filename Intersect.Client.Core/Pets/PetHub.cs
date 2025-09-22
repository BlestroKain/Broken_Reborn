using System;
using System.Collections.Generic;
using System.Globalization;
using Intersect.Client.Entities;
using Intersect.Client.General;
using Intersect.Client.Interface;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Pets;
using Intersect.Network.Packets.Client;
using Intersect.Network.Packets.Server;
using Intersect;
using Intersect.Localization;

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
    private bool _invokeRequested;
    private bool _dismissRequested;
    private readonly Dictionary<Guid, PetProgressSnapshot> _progressSnapshots = new();

    public PetHub()
    {
        Globals.PetMetadataChanged += OnPetMetadataChanged;
        Globals.PetProgressChanged += OnPetProgressChanged;
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
        var petName = string.Empty;

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

            _invokeRequested = true;
            petName = GetEquippedPetDisplayName();
        }

        Intersect.Client.Networking. Network.SendPacket(new SpawnPetRequestPacket(openPetHub));
        QueuePetMessage(Strings.Pets.SummonRequested, petName);
        return true;
    }

    public bool DismissPet(bool closePetHub = false)
    {
        var petName = string.Empty;

        lock (_syncRoot)
        {
            if (!_isSpawnRequested)
            {
                return false;
            }

            _dismissRequested = true;
            petName = GetEquippedPetDisplayName();
        }

        Intersect.Client.Networking. Network.SendPacket(new DespawnPetRequestPacket(closePetHub));
        QueuePetMessage(Strings.Pets.DismissRequested, petName);
        return true;
    }

    public void HandlePetLeft(Guid petId)
    {
        bool activeChanged;
        bool behaviorChanged;
        Pet? previousPet = null;

        lock (_syncRoot)
        {
            if (_activePet?.Id != petId)
            {
                return;
            }

            previousPet = _activePet;
            (activeChanged, behaviorChanged) = UpdateState(null, PetState.Follow, true);
            if (previousPet != null)
            {
                _progressSnapshots.Remove(previousPet.Id);
            }
        }

        if (behaviorChanged)
        {
            BehaviorChanged?.Invoke();
        }

        if (activeChanged)
        {
            HandleActivePetChanged(previousPet, null);
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
        Pet? previousPet = null;
        Pet? currentPet = null;

        lock (_syncRoot)
        {
            if (!Globals.TryGetEntity(EntityType.Pet, packet.EntityId, out var entity) || entity is not Pet pet)
            {
                if (_activePet?.Id == packet.EntityId)
                {
                    previousPet = _activePet;
                    (activeChanged, behaviorChanged) = UpdateState(null, PetState.Follow, true);
                    if (previousPet != null)
                    {
                        _progressSnapshots.Remove(previousPet.Id);
                    }
                }

                goto RaiseEvents;
            }

            if (!pet.IsOwnedByLocalPlayer)
            {
                if (_activePet?.Id == pet.Id)
                {
                    previousPet = _activePet;
                    (activeChanged, behaviorChanged) = UpdateState(null, PetState.Follow, true);
                    if (previousPet != null)
                    {
                        _progressSnapshots.Remove(previousPet.Id);
                    }
                }

                goto RaiseEvents;
            }

            previousPet = _activePet;
            (activeChanged, behaviorChanged) = UpdateState(pet, packet.Behavior, true);
            currentPet = _activePet;
            if (currentPet != null)
            {
                _progressSnapshots[currentPet.Id] = CreateSnapshot(currentPet);
            }
        }

    RaiseEvents:
        if (behaviorChanged)
        {
            BehaviorChanged?.Invoke();
        }

        if (activeChanged)
        {
            HandleActivePetChanged(previousPet, currentPet);
            ActivePetChanged?.Invoke();
        }
    }

    private void OnPetProgressChanged(Pet pet)
    {
        if (pet == null || pet.IsDisposed || !pet.IsOwnedByLocalPlayer)
        {
            return;
        }

        PetProgressSnapshot? previousSnapshot;
        PetProgressSnapshot newSnapshot;

        lock (_syncRoot)
        {
            previousSnapshot = GetSnapshot(pet.Id);
            newSnapshot = CreateSnapshot(pet);
            _progressSnapshots[pet.Id] = newSnapshot;
        }

        if (previousSnapshot == null)
        {
            return;
        }

        if (newSnapshot.Level > previousSnapshot.Level)
        {
            var petName = !string.IsNullOrWhiteSpace(newSnapshot.Name)
                ? newSnapshot.Name!
                : GetDescriptorName(newSnapshot.DescriptorId);
            QueuePetMessage(Strings.Pets.PetLevelGained, petName, newSnapshot.Level);
            return;
        }

        if (newSnapshot.Level == previousSnapshot.Level)
        {
            var experienceGained = newSnapshot.Experience - previousSnapshot.Experience;
            if (experienceGained > 0)
            {
                var petName = !string.IsNullOrWhiteSpace(newSnapshot.Name)
                    ? newSnapshot.Name!
                    : GetDescriptorName(newSnapshot.DescriptorId);
                QueuePetMessage(Strings.Pets.PetExperienceGained, petName, FormatNumber(experienceGained));
            }
        }
    }

    private void HandleActivePetChanged(Pet? previousPet, Pet? currentPet)
    {
        if (ReferenceEquals(previousPet, currentPet))
        {
            return;
        }

        if (currentPet != null)
        {
            var name = GetPetDisplayName(currentPet);
            QueuePetMessage(Strings.Pets.Summoned, name);
            _invokeRequested = false;
            _dismissRequested = false;
            return;
        }

        if (previousPet == null)
        {
            return;
        }

        var previousName = GetPetDisplayName(previousPet, previousPet.Descriptor, _equippedPetName);
        var health = previousPet.Vital.Length > (int)Vital.Health ? previousPet.Vital[(int)Vital.Health] : 0;

        if (_dismissRequested)
        {
            QueuePetMessage(Strings.Pets.Dismissed, previousName);
        }
        else if (health <= 0)
        {
            QueuePetMessage(Strings.Pets.PetDied, previousName);
        }
        else
        {
            QueuePetMessage(Strings.Pets.PetLost, previousName);
        }

        _dismissRequested = false;
        _invokeRequested = false;
    }

    private void HandleDescriptorChange(PetProgressSnapshot previousSnapshot, PetProgressSnapshot newSnapshot)
    {
        if (previousSnapshot.DescriptorId == newSnapshot.DescriptorId)
        {
            return;
        }

        var petName = !string.IsNullOrWhiteSpace(newSnapshot.Name)
            ? newSnapshot.Name!
            : !string.IsNullOrWhiteSpace(previousSnapshot.Name)
                ? previousSnapshot.Name!
                : GetDescriptorName(previousSnapshot.DescriptorId);
        var newDescriptorName = GetDescriptorName(newSnapshot.DescriptorId);
        QueuePetMessage(Strings.Pets.PetEvolved, petName, newDescriptorName);
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
            _invokeRequested = false;
            _dismissRequested = false;
            _progressSnapshots.Clear();
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
        Pet? previousPet = null;
        Pet? currentPet = null;
        PetProgressSnapshot? previousSnapshot = null;
        PetProgressSnapshot? newSnapshot = null;

        lock (_syncRoot)
        {
            if (pet.IsOwnedByLocalPlayer)
            {
                previousPet = _activePet;
                previousSnapshot = GetSnapshot(pet.Id);
                (activeChanged, behaviorChanged) = UpdateState(pet, pet.Behavior, true);
                currentPet = _activePet;
                if (currentPet != null)
                {
                    newSnapshot = CreateSnapshot(currentPet);
                    _progressSnapshots[currentPet.Id] = newSnapshot;
                }
            }
            else if (_activePet?.Id == pet.Id)
            {
                previousPet = _activePet;
                (activeChanged, behaviorChanged) = UpdateState(null, PetState.Follow, true);
                currentPet = null;
                if (previousPet != null)
                {
                    _progressSnapshots.Remove(previousPet.Id);
                }
            }
        }

        if (pet.IsOwnedByLocalPlayer && previousSnapshot != null && newSnapshot != null)
        {
            HandleDescriptorChange(previousSnapshot, newSnapshot);
        }

        if (behaviorChanged)
        {
            BehaviorChanged?.Invoke();
        }

        if (activeChanged)
        {
            HandleActivePetChanged(previousPet, currentPet);
            ActivePetChanged?.Invoke();
        }
    }
    public void SyncEquippedPet(Player? player)
    {
        try
        {
            // Blindajes de arranque
            if (player == null || player.IsDisposed)
            {
                // Si no hay player válido, y hay una pet activa, aseguremos estado coherente
                if (HasActivePet && ActivePet != null && !ActivePet.IsDisposed)
                {
                    _ = DismissPet();
                }

                UpdateEquippedPet(null, null);
                return;
            }

            var inventory = player.Inventory;
            var equipment = player.MyEquipment;

            if (inventory == null || equipment == null)
            {
                // No podemos resolver aún el equipamiento; no rompas el arranque
                return;
            }

            PetDescriptor? foundDescriptor = null;
            string? foundPetName = null;

            // Buscar en TODO el equipamiento cualquier ítem que tenga datos de Pet válidos
            foreach (var slotIndices in equipment.Values)
            {
                if (slotIndices == null)
                {
                    continue;
                }

                foreach (var slotIndex in slotIndices)
                {
                    if (slotIndex < 0 || slotIndex >= inventory.Length)
                    {
                        continue;
                    }

                    var item = inventory[slotIndex];
                    var itemDescriptor = item?.Descriptor;
                    var petData = itemDescriptor?.Pet;

                    if (petData == null || petData.PetDescriptorId == Guid.Empty)
                    {
                        continue;
                    }

                    // Intentar resolver el descriptor de la mascota
                    var descriptor = petData.Descriptor;
                    if (descriptor == null)
                    {
                        // Catálogo aún no cargado completamente — salta sin fallar
                        continue;
                    }

                    foundDescriptor = descriptor;
                    foundPetName = string.IsNullOrWhiteSpace(petData.PetNameOverride) ? null : petData.PetNameOverride;

                    // Ya encontramos uno válido; salimos
                    goto FoundDescriptor;
                }
            }

FoundDescriptor:
// Caso: NO hay mascota equipada actualmente (se quitó el item)
            if (foundDescriptor == null)
            {
                // Si hay una pet activa, despawnea
                if (HasActivePet && ActivePet != null && !ActivePet.IsDisposed)
                {
                    _ = DismissPet();
                }

                // Limpia el descriptor/nombre equipados en el hub
                UpdateEquippedPet(null, null);

                // No seguir
                return;
            }

            // Caso: SÍ hay mascota equipada
            // Si ya hay pet activa y el descriptor cambió, primero despawnea para evitar duplicados
            if (HasActivePet && ActivePet != null && !ActivePet.IsDisposed)
            {
                var activeDescId = ActivePet.Descriptor?.Id ?? Guid.Empty;
                if (activeDescId != foundDescriptor.Id)
                {
                    _ = DismissPet();
                }
            }

            // Si ya teníamos un descriptor equipado y es el mismo, solo refresca el nombre (no dispares nada más)
            // Nota: si tu PetHub expone EquippedDescriptor, úsalo para comparar y evitar trabajo innecesario
            if (EquippedDescriptor != null && EquippedDescriptor.Id == foundDescriptor.Id)
            {
                UpdateEquippedPet(foundDescriptor, foundPetName);
                return;
            }

            // Actualiza el descriptor/nombre equipados en el hub
            UpdateEquippedPet(foundDescriptor, foundPetName);

            // Importante: NO auto-invocar aquí para evitar duplicados si otro flujo ya invoca.
            // El usuario podrá usar el botón "Invocar" o tu lógica externa decidirá cuándo hacerlo.
        }
        catch
        {
            // En cliente, evita tumbar el arranque por un NRE aquí.
            // (Opcional) Agrega logging si tu contexto lo permite.
            // ApplicationContext.CurrentContext?.Logger?.LogError(ex, "Error in SyncEquippedPet");
        }
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

    private sealed class PetProgressSnapshot
    {
        public Guid PetId { get; init; }

        public string? Name { get; init; }

        public Guid DescriptorId { get; init; }

        public int Level { get; init; }

        public long Experience { get; init; }

        public long ExperienceToNextLevel { get; init; }

        public PetProgressSnapshot Clone() => new()
        {
            PetId = PetId,
            Name = Name,
            DescriptorId = DescriptorId,
            Level = Level,
            Experience = Experience,
            ExperienceToNextLevel = ExperienceToNextLevel,
        };
    }

    private static PetProgressSnapshot CreateSnapshot(Pet pet) => new()
    {
        PetId = pet.Id,
        Name = pet.Name,
        DescriptorId = pet.Descriptor?.Id ?? Guid.Empty,
        Level = pet.Level,
        Experience = pet.Experience,
        ExperienceToNextLevel = pet.ExperienceToNextLevel,
    };

    private PetProgressSnapshot? GetSnapshot(Guid petId)
    {
        if (!_progressSnapshots.TryGetValue(petId, out var snapshot))
        {
            return null;
        }

        return snapshot.Clone();
    }

    private static string FormatNumber(long value) => value.ToString("N0", CultureInfo.CurrentCulture);

    private static string GetPetDisplayName(Pet? pet, PetDescriptor? descriptor = null, string? fallbackName = null)
    {
        if (!string.IsNullOrWhiteSpace(pet?.Name))
        {
            return pet!.Name!;
        }

        if (!string.IsNullOrWhiteSpace(fallbackName))
        {
            return fallbackName!;
        }

        descriptor ??= pet?.Descriptor;
        var descriptorName = descriptor?.Name;
        if (!string.IsNullOrWhiteSpace(descriptorName))
        {
            return descriptorName!;
        }

        return Strings.Pets.UnknownDescriptorName.ToString();
    }

    private string GetEquippedPetDisplayName()
    {
        if (_activePet is { IsDisposed: false } activePet)
        {
            return GetPetDisplayName(activePet);
        }

        return GetPetDisplayName(null, _equippedDescriptor, _equippedPetName);
    }

    private static void QueuePetMessage(LocalizedString template, params object[] args)
    {
        var message = template.ToString(args);
        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }

        ChatboxMsg.AddMessage(new ChatboxMsg(message, CustomColors.Alerts.Info, ChatMessageType.Notice));

    }

    private static string GetDescriptorName(Guid descriptorId)
    {
        if (descriptorId == Guid.Empty)
        {
            return Strings.Pets.UnknownDescriptorName.ToString();
        }

        var descriptor = PetDescriptor.Get(descriptorId);
        return !string.IsNullOrWhiteSpace(descriptor?.Name)
            ? descriptor!.Name!
            : Strings.Pets.UnknownDescriptorName.ToString();
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
