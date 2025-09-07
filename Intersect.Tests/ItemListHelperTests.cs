using System;
using System.Linq;
using Intersect.Client.Utilities;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Items;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Intersect;
using Intersect.Client.Items;

namespace Intersect.Tests;

[TestFixture]
public class ItemListHelperTests
{
    [SetUp]
    public void Setup()
    {
        Options.EnsureCreated();
    }

    [Test]
    public void FilterAndSort_IncludesValidItem()
    {
        var descriptor = new ItemDescriptor(Guid.NewGuid())
        {
            Name = "Sword",
            Price = 10,
            ItemType = ItemType.Equipment,
            Subtype = "Sword"
        };

        var items = new[] { (Descriptor: descriptor, Quantity: 1) };

        var result = ItemListHelper.FilterAndSort(
            items,
            x => x.Descriptor,
            x => x.Quantity,
            null,
            null,
            null,
            SortCriterion.Name,
            true
        ).ToList();

        Assert.AreEqual(1, result.Count);
        Assert.AreSame(descriptor, result[0].Descriptor);
    }

    [TestCase("", 10, ItemType.Equipment, "Sword", 1, TestName = "EmptyName")]
    [TestCase("Sword", -1, ItemType.Equipment, "Sword", 1, TestName = "NegativePrice")]
    [TestCase("Sword", 10, ItemType.Equipment, "Sword", -1, TestName = "NegativeQuantity")]
    [TestCase("Sword", 10, (ItemType)999, "Sword", 1, TestName = "InvalidItemType")]
    [TestCase("Sword", 10, ItemType.Equipment, "UnknownSubtype", 1, TestName = "UnknownSubtype")]
    public void FilterAndSort_ExcludesInvalidItems(string name, int price, ItemType type, string subtype, int quantity)
    {
        var descriptor = new ItemDescriptor(Guid.NewGuid())
        {
            Name = name,
            Price = price,
            ItemType = type,
            Subtype = subtype
        };

        var items = new[] { (Descriptor: descriptor, Quantity: quantity) };

        var result = ItemListHelper.FilterAndSort(
            items,
            x => x.Descriptor,
            x => x.Quantity,
            null,
            null,
            null,
            SortCriterion.Name,
            true
        );

        Assert.AreEqual(0, result.Count());
    }

    [Test]
    public void FilterAndSort_IncludesEquipmentSlot()
    {
        var descriptor = new ItemDescriptor(Guid.NewGuid())
        {
            Name = "Iron Helmet",
            Price = 5,
            ItemType = ItemType.Equipment,
            Subtype = "Helmet"
        };

        var items = new[] { (Descriptor: descriptor, Quantity: 1) };

        var result = ItemListHelper.FilterAndSort(
            items,
            x => x.Descriptor,
            x => x.Quantity,
            null,
            null,
            null,
            SortCriterion.Name,
            true
        ).ToList();

        Assert.AreEqual(1, result.Count);
        Assert.AreSame(descriptor, result[0].Descriptor);
    }

    [Test]
    public void CompareItems_SortsByNameAscending()
    {
        var descriptorA = new ItemDescriptor(Guid.NewGuid())
        {
            Name = "Alpha",
            Price = 1,
            ItemType = ItemType.Equipment,
            Subtype = "Sword"
        };

        var descriptorB = new ItemDescriptor(Guid.NewGuid())
        {
            Name = "Beta",
            Price = 1,
            ItemType = ItemType.Equipment,
            Subtype = "Sword"
        };

        ItemDescriptor.Lookup.Set(descriptorA.Id, descriptorA);
        ItemDescriptor.Lookup.Set(descriptorB.Id, descriptorB);

        var itemA = new Item();
        itemA.Load(descriptorA.Id, 1, null, new ItemProperties());

        var itemB = new Item();
        itemB.Load(descriptorB.Id, 1, null, new ItemProperties());

        var result = ItemListHelper.CompareItems(itemA, itemB, SortCriterion.Name, true);

        Assert.IsTrue(result < 0);
    }
}

