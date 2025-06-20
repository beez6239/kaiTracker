using System.Threading.Tasks;
using KaiCryptoTracker.AllApiCalls;
using KaiCryptoTracker.DbContext;
using KaiCryptoTracker.Helpers;
using KaiCryptoTracker.MarketDto;
using KaiCryptoTracker.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KaiCryptoTracker.TokenService;

public class TokenService : ITokenService
{
    private readonly IHttpClientFactory _httpclientFactory;
    private readonly IConfiguration _configuration;

    private readonly ILogger<TokenService> _logger;
    private readonly IApiCalls _apicalls;

    private readonly ApplicationDbContext _dbcontext;

    // private string url = "";

    public TokenService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<TokenService> logger, IApiCalls apicalls, ApplicationDbContext dbcontext)
    {
        _httpclientFactory = httpClientFactory;
        _configuration = configuration;
        _apicalls = apicalls;
        _dbcontext = dbcontext;
        _logger = logger;
    }

    public async Task<List<MarketDataDto>> CoinMarketDataAsync()  //Get all coins market data
    {
        string url = $"{_configuration.GetSection("CoinGecko")["url"]}markets?vs_currency=usd";
        string json = string.Empty;
        if (!string.IsNullOrEmpty(url))
        {
            json = await _apicalls.CoinGeckoAsync(url);

        }
        var marketdata = JsonConvert.DeserializeObject<List<MarketDataDto>>(json);

        return marketdata; 
   
    }

    public async Task<Dictionary<string, string>> GetAllBinanceSupportedTokensAsync()
    {
        string json = string.Empty;
        try
        {
            string? url = _configuration.GetSection("Binance")["url"];
            if (!string.IsNullOrEmpty(url))
            {
                string fullurl = $"{url}exchangeInfo";
                json = await _apicalls.BinanceAsync(fullurl);

                var coinlistdata = JObject.Parse(json);
                if (coinlistdata != null)
                {
                    //return filtered list of coins and return as dictionary (baseAsset as key )
                    return coinlistdata["symbols"]
                    .Where(s => s["status"].ToString() == "TRADING")
                    .GroupBy(b => b["baseAsset"].ToString().ToLower())
                    .ToDictionary(
                        k => k.Key,
                        k => k.FirstOrDefault()["symbol"].ToString()
                    );

                }
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        return null;
    }

    public async Task<List<Coins>> GetAllCoinGeckoSupportedTokensAsync()  //coin Gecko api to get all supported coins 
    {

        string json = string.Empty;

        try
        {
            string? url = _configuration.GetSection("CoinGecko")["url"];

            if (!string.IsNullOrEmpty(url))
            {
                string fullurl = $"{url}list?include_platform=false";
                json = await _apicalls.BinanceAsync(fullurl);

                //store response in jobject list first 
                var listofcoins = JsonConvert.DeserializeObject<List<JObject>>(json);

                //add needed data to (Coin) class 
                var coindatafromgecko = listofcoins.Select(c => new Coins
                {
                    CoinGeckoId = c["id"].ToString(),
                    Name = c["name"]?.ToString(),
                    Symbol = c["symbol"]?.ToString(),
                    BinanceSymbol = c["symbol"]?.ToString()

                }).ToList();

                if (coindatafromgecko != null)

                    return coindatafromgecko;

            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to write to Database ");
        }

        return null;
    }

    public Task GetAllTokenBalanceAsync(string walletaddress, string chain)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetTokenBalanceAsync(string walletaddress, string chain)
    {
        string json = string.Empty;
        try
        {

            string? url = _configuration.GetSection("Moralis")["Url"];

            string fullurl = $"{url}/{walletaddress}/erc20?chain={chain}";
            json = await _apicalls.MoralisAsync(fullurl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        return json;

    }

    public async Task<decimal> GetTokenCurrentPriceAsync(string TokenID)
    {
        string? url = $"{_configuration.GetSection("CoinGecko")["simpleurl"]}{TokenID}";

        var json = await _apicalls.CoinGeckoAsync(url);



        return 0m;
    }


    public Task GetTokenMetaDataAsync(string walletaddress, string chain)
    {
        throw new NotImplementedException();
    }


    // Function to merge supported coin metadata
    public async Task<List<Coins>> SupportedCoinsAfterMergeAsync()
    {
        var coinlistfrombinance = await GetAllBinanceSupportedTokensAsync();
        var coinlistfromgecko = await GetAllCoinGeckoSupportedTokensAsync();

        //filter only coins that is available in binance dictionary and map to list of coins type
        return coinlistfromgecko.Where(c => coinlistfrombinance.ContainsKey(c.Symbol))
        .Select(c => new Coins
        {
            BinanceSymbol = coinlistfrombinance[c.Symbol],
            CoinGeckoId = c.CoinGeckoId,
            Name = c.Name,
            Symbol = c.Symbol


        }).ToList();

    }


    //Function to get candle closing prices 
    public async Task<List<decimal>> GetCoinCandleDataAsync(string symbol, CandleInterval interval)
    {
        string kline = "klines";
        var url = $"{_configuration.GetSection("Binance")["url"]}{kline}?symbol={symbol}&interval={interval.ConvertBinanceString()}";
        var json = await _apicalls.BinanceAsync(url);
        
        var prices = JArray.Parse(json);
        List<decimal> closingprices = [];
        foreach (var price in prices)
        {
            var closingprice = Convert.ToDecimal(price[4]);

            closingprices.Add(HelperClass.FormatDigitToFourDecimalHelper(closingprice));

        }

        return closingprices;

    }

}