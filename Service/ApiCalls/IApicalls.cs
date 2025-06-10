
namespace KaiCryptoTracker.AllApiCalls;

public interface IApiCalls
{
    public Task<string> Moralis(string url);
    public Task<string> CoinGecko(string url);
    public Task<string> Binance(string url);

}