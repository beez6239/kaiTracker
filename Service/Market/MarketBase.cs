
using KaiCryptoTracker.MarketDto;
using KaiCryptoTracker.TokenService;

namespace KaiCryptoTracker.Market;

public abstract class MarketBase
{
    private readonly ITokenService _tokenservice;
    private readonly ILogger<MarketBase> _logger;
    public MarketBase(ITokenService tokenservice, ILogger<MarketBase> logger)
    {
        _tokenservice = tokenservice;
        _logger = logger;

    }
    // public abstract void GetRelativeStrengthIndex(int firstvalue, int secondvalue);
    public abstract Task<decimal> GetMovingAverage(string symbol, int interval, int candlecount);
    public abstract void GetCurrentPrice(string cointicker);
    public abstract Task<bool> CompareMovingAverage(int firstcandlecount, int secondcandlecount, string symbol, int interval);

    public async Task<List<MarketDataDto>> livemarketdata()
    {
        return await _tokenservice.CoinMarketDataAsync();
    }
    
    
}