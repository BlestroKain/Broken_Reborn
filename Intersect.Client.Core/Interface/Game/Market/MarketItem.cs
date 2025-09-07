using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface.Game;

namespace Intersect.Client.Interface.Game.Market;

public partial class MarketItem : SlotItem
{
    public MarketItem(Base parent, int index, ContextMenu contextMenu)
        : base(parent, nameof(MarketItem), index, contextMenu)
    {
    }
}
