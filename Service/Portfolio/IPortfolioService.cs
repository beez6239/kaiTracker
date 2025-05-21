using KaiCryptoTracker.Models; 

public interface IPortfolio
{
    Task<Portfolio> GetPortfolioOverviewAsync(string userId);

    Task<decimal> GetTotalValueAsync(string userId);
    
    Task<List<Chain>> GetHoldingsPerChainAsync(string userId);
}