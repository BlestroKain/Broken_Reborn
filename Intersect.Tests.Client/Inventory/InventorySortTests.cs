using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Intersect;
using Intersect.Client.Utilities;
using Intersect.Framework.Core.GameObjects.Items;
using NUnit.Framework;

namespace Intersect.Tests.Client.Inventory;

[TestFixture]
public class InventorySortTests
{
    [Test]
    public void FilterAndSort_SkipsInvalidItems()
    {
        var ensure = typeof(Options).GetMethod("EnsureCreated", BindingFlags.NonPublic | BindingFlags.Static);
        ensure!.Invoke(null, null);
        Options.Instance.Items.ItemSubtypes = new Dictionary<ItemType, List<string>>
        {
            { ItemType.Equipment, new() { "Sword" } }
        };

        var validA = new ItemDescriptor { Name = "A", ItemType = ItemType.Equipment, Subtype = "Sword", Price = 1 };
        var invalid = new ItemDescriptor { Name = string.Empty, ItemType = ItemType.Equipment, Subtype = "Sword", Price = 1 };
        var validB = new ItemDescriptor { Name = "B", ItemType = ItemType.Equipment, Subtype = "Sword", Price = 1 };

        var items = new[] { validA, invalid, validB };

        var sorted = ItemListHelper.FilterAndSort(
            items,
            d => d,
            d => 1,
            searchText: null,
            type: null,
            subtype: null,
            criterion: SortCriterion.Name,
            ascending: true
        ).ToList();

        Assert.That(sorted, Is.EqualTo(new[] { validA, validB }));
    }
}
