using KaiCryptoTracker.Helpers;
using KaiCryptoTracker.TokenService;

namespace KaiCryptoTracker.Market;

public class MartketData : MarketBase
{
    private readonly ITokenService _tokenservice;
    private readonly ILogger<MartketData> _logger;
    public MartketData(ITokenService tokenService, ILogger<MartketData> logger) : base(tokenService, logger)
    {
        _tokenservice = tokenService;
        _logger = logger;
    }


   //Compare moving average 
    public async override Task<bool> CompareMovingAverage(int firstcandlecount, int secondcandlecount, string symbol, int interval)
    {
        
        var task = new List<Task<decimal>>
        {
            GetMovingAverage(symbol, interval, firstcandlecount),
            GetMovingAverage(symbol, interval, secondcandlecount)
        };

        try
        {
            var t = await Task.WhenAll(task);

            return HelperClass.FormatDigitToFourDecimalHelper(t[0]) > HelperClass.FormatDigitToFourDecimalHelper(t[1]); 
        }
        catch (Exception ex)
        {
            _logger.LogError("{0} getting moving average attempts failed ", nameof(task));
        }
       

        return false;
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