
namespace KaiCryptoTracker.AllApiCalls;

public interface IApiCalls
{
    public Task<string> Moralis(string url);
}