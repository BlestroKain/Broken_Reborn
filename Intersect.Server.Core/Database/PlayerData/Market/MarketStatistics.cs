namespace Intersect.Server.Database.PlayerData.Market;

public class MarketStatistics
{
    private long _minPrice = long.MaxValue;
    private long _maxPrice = long.MinValue;
    private long _totalPrice;
    private int _count;

    public void Record(long price)
    {
        if (price < _minPrice)
        {
            _minPrice = price;
        }

        if (price > _maxPrice)
        {
            _maxPrice = price;
        }

        _totalPrice += price;
        _count++;
    }

    public long SuggestedPrice => _count > 0 ? _totalPrice / _count : 0;

    public long MinPrice => _count > 0 ? _minPrice : 0;

    public long MaxPrice => _count > 0 ? _maxPrice : 0;
}

