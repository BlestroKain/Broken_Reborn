using System.Collections.Generic;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Network.Packets.Server;

namespace Intersect.Client.Interface.Game.Market;

public partial class MarketWindow : Window
{
    private readonly List<MarketItem> _items = new();

    public MarketWindow(Canvas gameCanvas) : base(gameCanvas, Strings.Market.Title, false, nameof(MarketWindow))
    {
        DisableResizing();
        Alignment = [Alignments.Center];
        IsResizable = false;
        IsClosable = true;
    }

    public void LoadListings(List<MarketListingPacket> listings)
    {
        foreach (var listing in listings)
        {
            var marketItem = new MarketItem(this, _items.Count, new ContextMenu(this));
            marketItem.Load(listing.ListingId, listing.SellerId, listing.ItemId, listing.Properties);
            _items.Add(marketItem);
        }
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
    }
}
