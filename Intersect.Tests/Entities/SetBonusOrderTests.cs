using System;
using System.Collections.Generic;
using Intersect.Config;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Server.Database;
using Intersect.Server.Entities;
using NUnit.Framework;

namespace Intersect.Tests.Entities;

public class SetBonusOrderTests
{
    [SetUp]
    public void Setup()
    {
        Options.EnsureCreated();
    }

    [Test]
    public void ApplySetBonuses_IsOrderIndependent()
    {
        var setA = new SetDescriptor(Guid.NewGuid()) { Name = "A" };
        setA.PercentageStats[(int)Stat.Attack] = 10;
        SetDescriptor.Lookup[setA.Id] = setA;

        var setB = new SetDescriptor(Guid.NewGuid()) { Name = "B" };
        setB.PercentageStats[(int)Stat.Attack] = 20;
        SetDescriptor.Lookup[setB.Id] = setB;

        var itemA = new ItemDescriptor(Guid.NewGuid())
        {
            ItemType = ItemType.Equipment,
            SetId = setA.Id
        };
        ItemDescriptor.Lookup[itemA.Id] = itemA;

        var itemB = new ItemDescriptor(Guid.NewGuid())
        {
            ItemType = ItemType.Equipment,
            SetId = setB.Id
        };
        ItemDescriptor.Lookup[itemB.Id] = itemB;

        var player = new Player();
        player.TryGetSlot(0, out var slot0, true);
        slot0.Set(new Item(itemA.Id, 1));
        player.TryGetSlot(1, out var slot1, true);
        slot1.Set(new Item(itemB.Id, 1));

        var ringSlot = Options.Instance.Equipment.Slots.IndexOf("Ring");

        player.Equipment[ringSlot] = new List<int> { 0, 1 };
        Player.ApplySetBonuses(player);
        var first = player.GetItemStatBuffs(Stat.Attack).Item2;

        player.Equipment[ringSlot] = new List<int> { 1, 0 };
        Player.ApplySetBonuses(player);
        var second = player.GetItemStatBuffs(Stat.Attack).Item2;

        Assert.That(first, Is.EqualTo(30));
        Assert.That(second, Is.EqualTo(30));
    }
}
