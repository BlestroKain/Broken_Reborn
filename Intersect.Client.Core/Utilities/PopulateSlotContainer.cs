using System;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface.Game;

namespace Intersect.Client.Utilities;

public static class PopulateSlotContainer
{
    // PopulateSlotContainer.cs (solo reemplaza el método Populate)
    public static void Populate(ScrollControl slotContainer, List<SlotItem> items)
    {
        float containerInnerWidth = slotContainer.InnerPanel.InnerWidth;
        var visibleIndex = 0;
        for (var slotIndex = 0; slotIndex < items.Count; slotIndex++)
        {
            var slot = items[slotIndex];
            if (!slot.IsVisibleInParent)
            {
                continue;
            }

            var outerSize = slot.OuterBounds.Size;
            var itemWidth = outerSize.X;
            if (itemWidth <= 0)
            {
                continue;
            }

            var itemsPerRow = Math.Max(1, (int)(containerInnerWidth / itemWidth));

            var column = visibleIndex % itemsPerRow;
            var row = visibleIndex / itemsPerRow;

            var xPosition = column * outerSize.X + slot.Margin.Left;
            var yPosition = row * outerSize.Y + slot.Margin.Top;

            slot.SetPosition(xPosition, yPosition);
            visibleIndex++;
        }
    }

}
