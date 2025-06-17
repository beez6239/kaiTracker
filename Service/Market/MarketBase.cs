
using KaiCryptoTracker.TokenService;

namespace KaiCryptoTracker.Market;

public abstract class MarketBase
{
    private readonly ITokenService _tokenservice;
    public MarketBase(ITokenService tokenservice)
    {
        _tokenservice = tokenservice;
    }
    // public abstract void GetRelativeStrengthIndex(int firstvalue, int secondvalue);
    public abstract Task<decimal> GetMovingAverage(string symbol, int interval, int candlecount);
    public abstract void GetCurrentPrice(string cointicker);
    public abstract Task<bool> CompareMovingAverage(int firstcandlecount, int secondcandlecount, string symbol, int interval); 
    
    
}