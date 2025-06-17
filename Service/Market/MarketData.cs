using KaiCryptoTracker.Helpers;
using KaiCryptoTracker.TokenService;

namespace KaiCryptoTracker.Market;

public class MartketData : MarketBase
{
    private readonly ITokenService _tokenservice;
    public MartketData(ITokenService tokenService) : base(tokenService)
    {
        _tokenservice = tokenService;
    }


   //Compare moving average 
    public async override Task<bool> CompareMovingAverage(int firstcandlecount, int secondcandlecount, string symbol, int interval)
    {
        var task = new[]
        {
            await GetMovingAverage(symbol,interval, firstcandlecount ),
            await GetMovingAverage(symbol,interval, secondcandlecount )
        };

        return task[0] > task[1];
    }


    public override void GetCurrentPrice(string cointicker)
    {
        throw new NotImplementedException();
    }


    //Function to get moving average 
    public async override Task<decimal> GetMovingAverage(string symbol, int interval, int candlecount)
    {
        var inputinterval = HelperClass.InputIntervals(interval);
        var prices = await _tokenservice.GetCoinCandleDataAsync(symbol, inputinterval);

        var lastcandlecountprices = prices.TakeLast(candlecount);

        return lastcandlecountprices.Average();

    }

    
    
}