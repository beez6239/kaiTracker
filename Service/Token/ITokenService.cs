namespace KaiCryptoTracker.TokenService;

public interface ITokenService
{
    public Task GetTokenBalanceAsync(string walletaddress, string chain);
    public Task GetAllTokenBalanceAsync(string walletaddress, string chain);
    public Task GetTokenMetaData(string walletaddress, string chain);
    
}