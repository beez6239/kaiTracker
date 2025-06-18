using KaiCryptoTracker.DbContext;
using KaiCryptoTracker.Models;
using Newtonsoft.Json;

using KaiCryptoTracker.ApiModels;
using KaiCryptoTracker.AllApiCalls;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<Wallet>> GetUserWalletsAsync(Guid userId)
    {
        return await _dbcontext.Wallets.Where(w => w.UserId == userId).ToListAsync();       
    }

    public async Task<WalletStatDto?> GetWalletDetailsAsync(string walletAddress, string chain)
    {
        string? url = $"{_configuration.GetSection("Moralis")["walleturl"]}{walletAddress}/stats?chain={chain}";
        var json = await _apicalls.MoralisAsync(url);
        
        return JsonConvert.DeserializeObject<WalletStatDto>(json);

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

            var json = await _apicalls.MoralisAsync(url);
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

            var json = await _apicalls.MoralisAsync(url);

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

    public async Task<TokenBalanceByWallet> GetTokenBalanceByWallet(string walletaddress, string chain)
    {
        TokenBalanceByWallet tokenbalancebywallet = null;
        try
        {
            string url = $"{_configuration.GetSection("Moralis")["Url"]}{walletaddress}/tokens?chain={chain}";

            var json = await _apicalls.MoralisAsync(url);
            tokenbalancebywallet = JsonConvert.DeserializeObject<TokenBalanceByWallet>(json);
            if (tokenbalancebywallet != null)
            {
                return tokenbalancebywallet;
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        return tokenbalancebywallet;
    }
}