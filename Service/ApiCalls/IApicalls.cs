
namespace KaiCryptoTracker.AllApiCalls;

public interface IApiCalls
{
    public Task<string> MoralisAsync(string url);
    public Task<string> CoinGeckoAsync(string url);
    public Task<string> BinanceAsync(string url);

}