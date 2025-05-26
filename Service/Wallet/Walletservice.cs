using KaiCryptoTracker.DbContext;
using KaiCryptoTracker.Models;
using Newtonsoft.Json;

using KaiCryptoTracker.ApiModels;

namespace KaiCryptoTracker.WalletService;

public class WalletService : IWalletService
{
    private readonly ApplicationDbContext _dbcontext;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpclientFactory;
    private readonly ILogger<WalletService> _logger;
    public WalletService(ApplicationDbContext dbcontext, ILogger<WalletService> logger, IConfiguration configuration, IHttpClientFactory httpclientFactory)
    {
        _dbcontext = dbcontext;
        _configuration = configuration;
        _httpclientFactory = httpclientFactory;
        _logger = logger;
    }
    public async Task AddWalletAsync(Guid userId, string walletaddress, string chain, string Walletname)
    {
        try
        {
            var wallet_to_add = new Wallet()
            {
                WalletName = Walletname,
                WalletAddress = walletaddress,
                UserId = userId
            };
            _dbcontext.Add(wallet_to_add);
            await _dbcontext.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    public Task<List<Wallet>> GetUserWalletsAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<Wallet> GetWalletDetailsAsync(Guid userId, string walletAddress, string chain)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveWalletAsync(Guid WalletId)
    {
        try
        {
            var wallet_to_delete = await _dbcontext.Wallets.FindAsync(WalletId);
            if (wallet_to_delete != null)
            {
                _dbcontext.Remove(wallet_to_delete);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }


        return false;
    }

    public Task<decimal> GetWalletPNLAsync(Guid WalletId)
    {
        throw new NotImplementedException();
    }

    public async Task<Activechains?> GetWalletActiveChains(string walletaddress)
    {
        Activechains activechains = null;
        
        try
        {
            string? url = _configuration.GetSection("Moralis")["walleturl"];

            string fullurl = $"{url}{walletaddress}/chains";

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

                activechains = JsonConvert.DeserializeObject<Activechains>(jsondata);
                if (activechains != null)
                {
                    return activechains;
                }

            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        return activechains;
        
    }

    public async Task GetWalletStats(string walletaddress, string chain)
    { 
         try
        {
            string? url = _configuration.GetSection("Moralis")["Url"];

            string fullurl = $"{url}{walletaddress}/stats?chain={chain}";

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
}