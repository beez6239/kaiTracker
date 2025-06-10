using KaiCryptoTracker.Models;

namespace KaiCryptoTracker.TokenService;

public interface ITokenService
{
    public Task<string> GetTokenBalanceAsync(string walletaddress, string chain);
    public Task GetAllTokenBalanceAsync(string walletaddress, string chain);

    public Task GetTokenMetaDataAsync(string walletaddress, string chain);

    //Coin Methods (Coin Gecko)
    public Task<List<Coins>> GetAllCoinGeckoSupportedTokensAsync();

    public Task<List<Coins>> SupportedCoinsAfterMergeAsync();


    public Task<Dictionary<string, string>> GetAllBinanceSupportedTokensAsync();

    public Task<decimal> GetTokenCurrentPriceAsync(string TokenID);  //Token ID should be coingecko token id 
    
     Task AddListOfSupportedTokensToDb();

    
}