using System.Threading.Tasks;
using KaiCryptoTracker.AllApiCalls;
using KaiCryptoTracker.DbContext;
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

    public async Task GetAllSupportedTokens()
    {
    
        string json = string.Empty;
        try
        {
            string? url = _configuration.GetSection("CoinGecko")["url"];

            string fullurl = $"{url}list?include_platform=false";
            json = await _apicalls.CoinGecko(fullurl);

            Coins[]? coinlist = JsonConvert.DeserializeObject<Coins[]>(json);
            if (coinlist != null)
            {
                await _dbcontext.AddRangeAsync(coinlist);
                await _dbcontext.SaveChangesAsync();
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to write to Database ");
        }
    
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

    public async Task<decimal> GetTokenCurrentPrice(string TokenID)
    {
         string? url = $"{_configuration.GetSection("CoinGecko")["simpleurl"]}{TokenID}";

         var json = await _apicalls.CoinGecko(url);
         
         
         
        return 0m;
    }


    public Task GetTokenMetaData(string walletaddress, string chain)
    {
        throw new NotImplementedException();
    }

  
}