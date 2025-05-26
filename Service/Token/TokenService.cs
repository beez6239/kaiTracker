using System.Threading.Tasks;

namespace KaiCryptoTracker.TokenService;

public class TokenService : ITokenService
{
    private readonly IHttpClientFactory _httpclientFactory;
    private readonly IConfiguration _configuration;

    private readonly ILogger<TokenService> _logger;

    // private string url = "";

    public TokenService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<TokenService> logger)
    {
        _httpclientFactory = httpClientFactory;
        _configuration = configuration;
        _logger = logger;
    }

    public Task GetAllTokenBalanceAsync(string walletaddress, string chain)
    {
        throw new NotImplementedException();
    }

    public async Task GetTokenBalanceAsync(string walletaddress, string chain)
    {
    
        try
        {

            string? url = _configuration.GetSection("Moralis")["Url"];

            string fullurl = $"{url}/{walletaddress}/erc20?chain={chain}";
            var client = _httpclientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, fullurl);
            request.Headers.Accept.ParseAdd("application/json");

            //get api key 
            string? apikey = _configuration.GetSection("Moralis")["key"];

            if (apikey != null) request.Headers.Add("X-API-Key", apikey);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var streamcontent = response.Content.ReadAsStream();
                var reader = new StreamReader(streamcontent);
                string jsondata = await reader.ReadToEndAsync();
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

    }


    public Task GetTokenMetaData(string walletaddress, string chain)
    {
        throw new NotImplementedException();
    }
}