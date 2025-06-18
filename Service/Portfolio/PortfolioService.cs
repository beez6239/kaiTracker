using KaiCryptoTracker.DbContext;
using Microsoft.EntityFrameworkCore;
using KaiCryptoTracker.Models;
using KaiCryptoTracker.PortfolioService;
using KaiCryptoTracker.AllApiCalls;
using KaiCryptoTracker.WalletService;

public class PortfolioService : IPortfolioService
{
    private readonly ApplicationDbContext _dbcontext;
    private readonly IApiCalls _api;
    private readonly IWalletService _walletservice; 
    private readonly ILogger<PortfolioService> _logger;
    public PortfolioService(ApplicationDbContext dbcontext, ILogger<PortfolioService> logger, IApiCalls api, IWalletService walletservice)
    {
        _dbcontext = dbcontext;
        _logger = logger;
        _api = api;
        _walletservice = walletservice; 
       
    }
    public async Task<Portfolio> AddPortfolioAsync(Guid userId, string? portfolioname)
    {
        if (string.IsNullOrEmpty(portfolioname)) portfolioname = "Default";
        var portfolio = new Portfolio()
        {
            Chains = new List<Chain>() { },
            Wallets = new List<Wallet>() { },
            PortfolioName = portfolioname,
            TotalTokens = 0,
            TotalValueUsd = 0,
            TotalWallets = 0,
            UserId = userId,

        };
        
        _dbcontext.Add(portfolio);
        await _dbcontext.SaveChangesAsync();

        return portfolio;
       
    }

    public Task DeletePortfolioAsync(Guid userId, Guid PortfolioId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Chain>> GetHoldingsPerChainAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<Portfolio> GetPortfolioOverviewAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<Portfolio> UpdatePortfolio(Guid userId, Guid PortfolioId, Guid WalletId)
    {

        // _walletservice.GetWalletActiveChains();
        throw new NotImplementedException();
    }

    // public async Task<decimal> GetTotalValueAsync(Guid userId)
    // {
    //      var user = await _dbcontext.Users
    //        .Include(u => u.Wallets)
    //         .ThenInclude(w => w.tokenHoldings)
    //         .ThenInclude(th => th.TokenMetadata)
    //        .FirstOrDefaultAsync(u => u.Id == userId);

    // if (user == null)
    //     throw new ArgumentException("User not found");

    // decimal totalValue = 0;

    // foreach (var wallet in user.Wallets)
    // {
    //     foreach (var holding in wallet.tokenHoldings)
    //     {

    //         var priceUsd = await _api.GetPriceCoinGecko(holding.TokenMetadata.Address, holding.TokenMetadata.Chain.ChainName);
    //         totalValue += holding.Balance * priceUsd;
    //     }
    // }

    // return totalValue;
    // }
}