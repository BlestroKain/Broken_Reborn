using System;
using System.Linq;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Framework.Core.GameObjects.Pets;
using Intersect.Server.Database;
using Intersect.Server.Entities;
using NUnit.Framework;

namespace Intersect.Tests.Server.Entities;

public partial class PlayerTests
{
    private static int GetPetEquipmentSlotIndex()
    {
        return Options.Instance.Equipment.EquipmentSlots.FindIndex(
            slot => string.Equals(slot.Name, "Pet", StringComparison.OrdinalIgnoreCase)
        );
    }

    private (Player player, ItemDescriptor itemDescriptor, PetDescriptor petDescriptor, int inventorySlot) CreatePlayerWithPetItem(
        bool summonOnEquip = true,
        bool despawnOnUnequip = true
    )
    {
        var player = new Player
        {
            MapId = _mapId,
            MapInstanceId = Guid.NewGuid(),
        };

        var statCount = Enum.GetValues<Stat>().Length;
        var vitalCount = Enum.GetValues<Vital>().Length;

        var petDescriptor = new PetDescriptor(Guid.NewGuid())
        {
            Name = "Test Pet",
            Level = 1,
            MaxLevel = 5,
            Experience = 0,
            Stats = Enumerable.Repeat(5, statCount).ToArray(),
            MaxVitals = Enumerable.Repeat(20L, vitalCount).ToArray(),
        };

        PetDescriptor.Lookup[petDescriptor.Id] = petDescriptor;

        var petData = new PetItemData
        {
            PetDescriptorId = petDescriptor.Id,
            SummonOnEquip = summonOnEquip,
            DespawnOnUnequip = despawnOnUnequip,
        };

        var petSlotIndex = GetPetEquipmentSlotIndex();
        Assert.That(petSlotIndex, Is.GreaterThanOrEqualTo(0), "Pet equipment slot must be configured for tests.");

        var itemDescriptor = new ItemDescriptor(Guid.NewGuid())
        {
            Name = "Pet Charm",
            ItemType = ItemType.Equipment,
            EquipmentSlot = petSlotIndex,
            Pet = petData,
        };

        ItemDescriptor.Lookup[itemDescriptor.Id] = itemDescriptor;

        const int inventorySlot = 0;
        player.Items[inventorySlot].Set(new Item(itemDescriptor.Id, 1));

        return (player, itemDescriptor, petDescriptor, inventorySlot);
    }

    [Test]
    public void EquipPetItemSetsActivePetAndRequestsSpawn()
    {
        var (player, itemDescriptor, petDescriptor, inventorySlot) = CreatePlayerWithPetItem();

        player.EquipItem(itemDescriptor, inventorySlot);

        Assert.Multiple(
            () =>
            {
                Assert.That(player.ActivePet, Is.Not.Null, "Active pet should be set after equipping pet item.");
                Assert.That(player.ActivePet!.PetDescriptorId, Is.EqualTo(petDescriptor.Id));
                Assert.That(player.ActivePet.PlayerId, Is.EqualTo(player.Id));
                Assert.That(player.ActivePet.BaseStats.Length, Is.EqualTo(Enum.GetValues<Stat>().Length));
                Assert.That(player.ActivePet.MaxVitals.Length, Is.EqualTo(Enum.GetValues<Vital>().Length));
                Assert.That(player.ActivePet.Vitals.Length, Is.EqualTo(Enum.GetValues<Vital>().Length));
                Assert.That(player.IsPetSpawnedViaHub, Is.True, "Equipping should request the pet hub spawn.");
            }
        );
    }

    [Test]
    public void UnequipPetItemClearsActivePetAndHubState()
    {
        var (player, itemDescriptor, _, inventorySlot) = CreatePlayerWithPetItem();

        player.EquipItem(itemDescriptor, inventorySlot);
        Assert.That(player.ActivePet, Is.Not.Null, "Active pet should be set before unequipping.");
        Assert.That(player.Pets.Count, Is.EqualTo(1), "Equipping should register the player pet once.");

        player.UnequipItem(itemDescriptor.Id);

        Assert.Multiple(
            () =>
            {
                Assert.That(player.ActivePet, Is.Null);
                Assert.That(player.ActivePetId, Is.Null);
                Assert.That(player.IsPetSpawnedViaHub, Is.False);
            }
        );

        player.EquipItem(itemDescriptor, inventorySlot);
        Assert.That(player.ActivePet, Is.Not.Null, "Active pet should be restored after re-equipping.");
        Assert.That(player.Pets.Count, Is.EqualTo(1), "Re-equipping should reuse the existing player pet record.");

        player.UnequipItem(itemDescriptor.EquipmentSlot);

        Assert.Multiple(
            () =>
            {
                Assert.That(player.ActivePet, Is.Null);
                Assert.That(player.ActivePetId, Is.Null);
                Assert.That(player.IsPetSpawnedViaHub, Is.False);
            }
        );
    }
}
