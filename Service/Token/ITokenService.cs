using KaiCryptoTracker.MarketDto;
using KaiCryptoTracker.Models;

namespace KaiCryptoTracker.TokenService;

public interface ITokenService
{

    //Coin method Moralis
    public Task<string> GetTokenBalanceAsync(string walletaddress, string chain);
    public Task GetAllTokenBalanceAsync(string walletaddress, string chain);

    public Task GetTokenMetaDataAsync(string walletaddress, string chain);

    //Coin Methods (Coin Gecko)
    public Task<List<Coins>> GetAllCoinGeckoSupportedTokensAsync();

    public Task<List<Coins>> SupportedCoinsAfterMergeAsync();

    public Task<List<MarketDataDto>> CoinMarketDataAsync(); 

    //Coin methods Binance 
    public Task<Dictionary<string, string>> GetAllBinanceSupportedTokensAsync();

    public Task<decimal> GetTokenCurrentPriceAsync(string TokenID);  //Token ID should be coingecko token id 

    public Task<List<decimal>> GetCoinCandleDataAsync(string symbol , CandleInterval interval); 
    

    
}