using System;
using System.Linq;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Framework.Core.GameObjects.Pets;
using Intersect.Server.Entities;
using Intersect.Shared.Pets;
using NUnit.Framework;

namespace Intersect.Tests.Server.Entities;

public partial class PlayerTests
{
    [Test]
    public void TestEquippingDuplicatePetItemsMaintainsIndependentProgress()
    {
        var petDescriptor = new PetDescriptor(Guid.NewGuid())
        {
            Name = "Test Pet",
        };
        PetDescriptor.Lookup.Set(petDescriptor.Id, petDescriptor);

        var itemDescriptor = new ItemDescriptor(Guid.NewGuid())
        {
            Name = "Pet Collar",
            ItemType = ItemType.Equipment,
            EquipmentSlot = 0,
            Pet = new PetItemData
            {
                PetDescriptorId = petDescriptor.Id,
                SummonOnEquip = false,
                DespawnOnUnequip = false,
                BindOnEquip = false,
            },
        };
        ItemDescriptor.Lookup.Set(itemDescriptor.Id, itemDescriptor);

        Player player = new()
        {
            MapId = _mapId,
        };

        Assert.That(player.TryGiveItem(itemDescriptor.Id, 1), Is.True);
        Assert.That(player.TryGiveItem(itemDescriptor.Id, 1), Is.True);

        var inventorySlots = player.FindInventoryItemSlots(itemDescriptor.Id);
        Assert.That(inventorySlots, Has.Count.GreaterThanOrEqualTo(2));

        var orderedSlots = inventorySlots.OrderBy(slot => slot.Slot).Take(2).ToArray();
        var firstSlot = orderedSlots[0];
        var secondSlot = orderedSlots[1];

        var firstSlotIndex = player.FindInventoryItemSlotIndex(firstSlot);
        var secondSlotIndex = player.FindInventoryItemSlotIndex(secondSlot);

        var firstInstanceId = firstSlot.PetInstanceId;
        var secondInstanceId = secondSlot.PetInstanceId;

        Assert.Multiple(
            () =>
            {
                Assert.That(firstInstanceId, Is.Not.Null);
                Assert.That(secondInstanceId, Is.Not.Null);
                Assert.That(firstInstanceId, Is.Not.EqualTo(secondInstanceId));
            }
        );

        player.EquipItem(itemDescriptor, firstSlotIndex, updateCooldown: false);

        Assert.That(player.ActivePet, Is.Not.Null);
        Assert.That(player.ActivePet!.PetInstanceId, Is.EqualTo(firstInstanceId));

        player.ActivePet.Level = 5;
        player.ActivePet.Experience = 50;

        player.UnequipItem(itemDescriptor.EquipmentSlot, sendUpdate: false);
        Assert.That(player.ActivePet, Is.Null);

        player.EquipItem(itemDescriptor, secondSlotIndex, updateCooldown: false);

        Assert.That(player.ActivePet, Is.Not.Null);
        Assert.That(player.ActivePet!.PetInstanceId, Is.EqualTo(secondInstanceId));

        player.ActivePet.Level = 3;
        player.ActivePet.Experience = 30;

        player.UnequipItem(itemDescriptor.EquipmentSlot, sendUpdate: false);

        player.EquipItem(itemDescriptor, firstSlotIndex, updateCooldown: false);

        Assert.That(player.ActivePet, Is.Not.Null);
        Assert.Multiple(
            () =>
            {
                Assert.That(player.ActivePet!.PetInstanceId, Is.EqualTo(firstInstanceId));
                Assert.That(player.ActivePet.Level, Is.EqualTo(5));
                Assert.That(player.ActivePet.Experience, Is.EqualTo(50));
            }
        );

        var storedSecondPet = player.Pets.Single(pet => pet.PetInstanceId == secondInstanceId);

        Assert.Multiple(
            () =>
            {
                Assert.That(storedSecondPet.Level, Is.EqualTo(3));
                Assert.That(storedSecondPet.Experience, Is.EqualTo(30));
            }
        );
    }

    [Test]
    public void TestUnequippingSummonedPetDespawnsActiveInstance()
    {
        var petDescriptor = new PetDescriptor(Guid.NewGuid())
        {
            Name = "Summoned Pet",
        };
        PetDescriptor.Lookup.Set(petDescriptor.Id, petDescriptor);

        var itemDescriptor = new ItemDescriptor(Guid.NewGuid())
        {
            Name = "Summoned Pet Collar",
            ItemType = ItemType.Equipment,
            EquipmentSlot = 0,
            Pet = new PetItemData
            {
                PetDescriptorId = petDescriptor.Id,
                SummonOnEquip = true,
                DespawnOnUnequip = false,
                BindOnEquip = false,
            },
        };
        ItemDescriptor.Lookup.Set(itemDescriptor.Id, itemDescriptor);

        Player player = new()
        {
            MapId = _mapId,
        };

        Assert.That(player.TryGiveItem(itemDescriptor.Id, 1), Is.True);

        var inventorySlot = player.FindInventoryItemSlots(itemDescriptor.Id).Single();
        var slotIndex = player.FindInventoryItemSlotIndex(inventorySlot);

        player.ActivePetMode = PetBehavior.Passive;

        player.EquipItem(itemDescriptor, slotIndex, updateCooldown: false);

        Assert.That(player.ActivePet, Is.Not.Null);
        var petInstanceId = player.ActivePet!.PetInstanceId;

        Assert.Multiple(
            () =>
            {
                Assert.That(player.ActivePetId, Is.EqualTo(player.ActivePet!.Id));
                Assert.That(player.CurrentPet, Is.Not.Null);
                Assert.That(player.CurrentPet!.Behavior, Is.EqualTo(player.ActivePetMode));
                Assert.That(player.SpawnedPets, Has.Count.EqualTo(1));
            }
        );

        player.UnequipItem(itemDescriptor.EquipmentSlot, sendUpdate: false);

        Assert.Multiple(
            () =>
            {
                Assert.That(player.ActivePet, Is.Null);
                Assert.That(player.ActivePetId, Is.Null);
                Assert.That(player.CurrentPet, Is.Null);
                Assert.That(player.SpawnedPets, Is.Empty);
                Assert.That(player.ActivePetMode, Is.EqualTo(PetBehavior.Passive));
            }
        );

        player.EquipItem(itemDescriptor, slotIndex, updateCooldown: false);

        Assert.Multiple(
            () =>
            {
                Assert.That(player.ActivePet, Is.Not.Null);
                Assert.That(player.ActivePet!.PetInstanceId, Is.EqualTo(petInstanceId));
                Assert.That(player.CurrentPet, Is.Not.Null);
                Assert.That(player.CurrentPet!.Behavior, Is.EqualTo(PetBehavior.Passive));
                Assert.That(player.SpawnedPets, Has.Count.EqualTo(1));
            }
        );
    }
}
