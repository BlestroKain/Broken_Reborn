using System.Collections.Generic;

namespace Intersect.Server.Database.PlayerData.Market;

public partial class MarketManager
{
    private readonly List<MarketListing> _listings = new();
    private readonly List<MarketTransaction> _transactions = new();

    public IReadOnlyList<MarketListing> Listings => _listings;
    public IReadOnlyList<MarketTransaction> Transactions => _transactions;

    public void AddListing(MarketListing listing) => _listings.Add(listing);

    public void RecordTransaction(MarketTransaction transaction) => _transactions.Add(transaction);
}
