using System.Threading.Tasks;
using KaiCryptoTracker.AllApiCalls;

namespace KaiCryptoTracker.TokenService;

public class TokenService : ITokenService
{
    private readonly IHttpClientFactory _httpclientFactory;
    private readonly IConfiguration _configuration;

    private readonly ILogger<TokenService> _logger;
    private readonly IApiCalls _apicalls; 

    // private string url = "";

    public TokenService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<TokenService> logger, IApiCalls apicalls)
    {
        _httpclientFactory = httpClientFactory;
        _configuration = configuration;
        _apicalls = apicalls;
        _logger = logger;
    }

    public async Task<string> GetAllSupportedTokens()
    {
    
        string json = string.Empty;
        try
        {
            string? url = _configuration.GetSection("CoinGecko")["url"];

            string fullurl = $"{url}list?include_platform=false";
            json = await _apicalls.CoinGecko(fullurl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        return json;
    
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
            json = await _apicalls.Moralis(fullurl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        return json;

    }


    public Task GetTokenMetaData(string walletaddress, string chain)
    {
        throw new NotImplementedException();
    }

  
}