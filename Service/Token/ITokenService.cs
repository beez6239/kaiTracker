namespace KaiCryptoTracker.TokenService;

public interface ITokenService
{
    public Task<string> GetTokenBalanceAsync(string walletaddress, string chain);
    public Task GetAllTokenBalanceAsync(string walletaddress, string chain);

    public Task GetTokenMetaData(string walletaddress, string chain);

    //Coin Methods (Coin Gecko)
    public Task GetAllSupportedTokens();

    public Task<decimal> GetTokenCurrentPrice(string TokenID);  //Token ID should be coingecko token id 

    
}