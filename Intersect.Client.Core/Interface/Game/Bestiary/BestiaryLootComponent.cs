using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Localization;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Framework.Core.GameObjects.NPCs;

namespace Intersect.Client.Interface.Game.Bestiary;

public partial class BestiaryLootComponent : ImagePanel
{
    private readonly Label _label;
    private bool _unlocked;

    public BestiaryLootComponent(Base parent, string name = "BestiaryLoot") : base(parent, name)
    {
        _label = new Label(this)
        {
            Text = Strings.Bestiary.Locked
        };
    }

    public void Unlock()
    {
        _unlocked = true;
        _label.Text = Strings.Bestiary.Unlocked;
    }

    public void Lock()
    {
        _unlocked = false;
        _label.Text = Strings.Bestiary.Locked;
    }

    public void CorrectWidth()
    {
        if (Parent == null)
        {
            return;
        }

        SetSize(Parent.InnerWidth - 20, Height);
    }

    public void ShowItemTooltip(ItemDescriptor item)
    {
        Interface.GameUi.ItemDescriptionWindow.Show(item, 1);
    }

    /// <summary>
    ///     Populates the component with the loot drops from the provided NPC
    ///     descriptor. Nested loot tables are expanded using
    ///     <see cref="LootTableHelpers"/> and the resulting aggregated chances are
    ///     displayed as text.
    /// </summary>
    /// <param name="npc">NPC whose loot should be displayed.</param>
    /// <param name="tableResolver">
    ///     Optional delegate used to resolve nested loot tables. When <c>null</c>
    ///     only the NPC's direct drops are considered.
    /// </param>
    public void SetLoot(NPCDescriptor npc, Func<Guid, IEnumerable<Drop>?>? tableResolver = null)
    {
        _unlocked = true;

        var expanded = LootTableHelpers.Expand(npc.Drops, tableResolver);
        var builder = new StringBuilder();

        foreach (var (itemId, chance) in expanded.OrderBy(kvp => kvp.Key))
        {
            var item = ItemDescriptor.Get(itemId);
            if (item == null)
            {
                continue;
            }

            if (builder.Length > 0)
            {
                builder.Append('\n');
            }

            builder.Append($"{item.Name}: {chance:P2}");
        }

        _label.Text = builder.Length > 0 ? builder.ToString() : "Unlocked";
    }
}
