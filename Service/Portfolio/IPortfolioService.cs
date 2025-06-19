using KaiCryptoTracker.ApiModels;
using KaiCryptoTracker.Models; 

namespace KaiCryptoTracker.PortfolioService;

public interface IPortfolioService
{
    public Task<Portfolio> GetPortfolioOverviewAsync(Guid userId);

    public Task<Portfolio> AddPortfolioAsync(Guid userId, string? portfolioname);
    public Task<Portfolio> UpdatePortfolio(Guid userId, Guid PortfolioId, Guid WalletId);

    public Task DeletePortfolioAsync(Guid userId, Guid PortfolioId);
    public Task<Dictionary<string, Activechains>> GetUserActiveChains(Guid userId);
    // Task<decimal> GetTotalValueAsync(Guid userId);

    public Task<List<AggregatedTokenHoldings>> GetUserTokenHoldingsAsync(Guid userId);

    public Task<Dictionary<string, TokenBalanceByWallet>> GetTokenBalance(Guid userId);

    public decimal GetPorfolioTotalValue(Dictionary<string, TokenBalanceByWallet> alltokenbalance);

    public Task SaveUserActiveChainsToPortfolioAsync(Guid userId, Guid portfolioId);

    public  Task<List<Chain>> GetAllUserChains(Guid PorfolioId);
}