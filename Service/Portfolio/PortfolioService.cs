using KaiCryptoTracker.DbContext;
using KaiCryptoTracker.Models;
using KaiCryptoTracker.PortfolioService;

public class PortfolioService : IPortfolioService
{
    private readonly ApplicationDbContext _dbcontext;
    private readonly ILogger<PortfolioService> _logger;
    public PortfolioService(ApplicationDbContext dbcontext, ILogger<PortfolioService> logger)
    {
        _dbcontext = dbcontext;
        _logger = logger;
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

    public Task<decimal> GetTotalValueAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}