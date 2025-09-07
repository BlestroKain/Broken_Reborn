using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Server.Entities;

namespace Intersect.Server.Database.PlayerData.Market;

public partial class MarketManager
{
    private readonly List<MarketListing> _listings = new();
    private readonly List<MarketTransaction> _transactions = new();

    public IReadOnlyList<MarketListing> Listings => _listings;
    public IReadOnlyList<MarketTransaction> Transactions => _transactions;

    public void AddListing(MarketListing listing) => _listings.Add(listing);

    public void RecordTransaction(MarketTransaction transaction) => _transactions.Add(transaction);

    public bool CancelListing(Player player, Guid listingId)
    {
        var listing = _listings.FirstOrDefault(l => l.Id == listingId && l.SellerId == player.Id);
        if (listing == null)
        {
            return false;
        }

        var itemGuid = ItemDescriptor.IdFromList(listing.ItemId);
        // Try to return the listed items, allowing overflow to bank or map. If nothing could be
        // returned we leave the listing intact so the player can try again later instead of
        // destroying the listing and its items.
        if (!player.TryGiveItem(itemGuid, listing.Quantity, listing.Properties, ItemHandling.Overflow, bankOverflow: true))
        {
            return false;
        }

        _listings.Remove(listing);
        return true;
    }
}
