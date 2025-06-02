using KaiCryptoTracker.Models; 

namespace KaiCryptoTracker.PortfolioService;
public interface IPortfolioService
{
    Task<Portfolio> GetPortfolioOverviewAsync(Guid userId);

    Task<Portfolio> AddPortfolioAsync(Guid userId, string? portfolioname);

    Task DeletePortfolioAsync(Guid userId, Guid PortfolioId);

    // Task<decimal> GetTotalValueAsync(Guid userId);

    Task<List<Chain>> GetHoldingsPerChainAsync(Guid userId);
}