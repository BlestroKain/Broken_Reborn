using Intersect.Client.Core;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Framework.Core.GameObjects.Items;

namespace Intersect.Client.Interface.Game.Market;

public partial class SellMarketWindow : Window
{
    private readonly int _slot;

    public SellMarketWindow(Canvas gameCanvas, int slot) : base(gameCanvas, Strings.Market.Title, false, nameof(SellMarketWindow))
    {
        _slot = slot;
        DisableResizing();
        Alignment = [Alignments.Center];
        IsResizable = false;
        IsClosable = true;
    }

    public ItemProperties? GetItemProperties()
    {
        return Globals.Me?.Inventory[_slot]?.ItemProperties;
    }

    public void SellItem(int quantity, long price)
    {
        PacketSender.SendCreateMarketListing(_slot, quantity, price);
    }
}
