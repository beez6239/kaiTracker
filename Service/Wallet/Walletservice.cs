using KaiCryptoTracker.DbContext;
using KaiCryptoTracker.Models;
using Newtonsoft.Json;

using KaiCryptoTracker.ApiModels;
using KaiCryptoTracker.AllApiCalls;

namespace KaiCryptoTracker.WalletService;

public class WalletService : IWalletService
{
    private readonly ApplicationDbContext _dbcontext;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpclientFactory;
    private readonly ILogger<WalletService> _logger;
    private readonly IApiCalls _apicalls; 
    public WalletService(ApplicationDbContext dbcontext, ILogger<WalletService> logger, IConfiguration configuration, IHttpClientFactory httpclientFactory , IApiCalls apicalls)
    {
        _dbcontext = dbcontext;
        _configuration = configuration;
        _httpclientFactory = httpclientFactory;
        _apicalls = apicalls;
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

    public async Task<WalletPnlSummary> GetWalletPNLAsync(string walletaddress, string chain)
    {
        WalletPnlSummary walletpnlsummary = null;
        try
        {
            string url = $"{_configuration.GetSection("Moralis")["walleturl"]}{walletaddress}/profitability/summary?chain={chain}";

            var json = await _apicalls.Moralis(url);
            walletpnlsummary = JsonConvert.DeserializeObject<WalletPnlSummary>(json);
            if (walletpnlsummary != null)
            {
                return walletpnlsummary;
            }


        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return walletpnlsummary;

    }

    public async Task<Activechains?> GetWalletActiveChains(string walletaddress)
    {
        Activechains activechains = null;
        
        try
        {
            string url = $"{_configuration.GetSection("Moralis")["walleturl"]}{walletaddress}/chains";

            var json = await _apicalls.Moralis(url);

              activechains = JsonConvert.DeserializeObject<Activechains>(json);
                if (activechains != null)
                {
                  return activechains;
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
            // string? url = _configuration.GetSection("Moralis")["Url"];

            string url = $"{_configuration.GetSection("Moralis")["Url"]}{walletaddress}/stats?chain={chain}";

            var json = await _apicalls.Moralis(url);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}