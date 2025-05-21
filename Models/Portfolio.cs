namespace KaiCryptoTracker.Models;
public class Portfolio
{
    public Guid UserId { get; set; }

    public decimal TotalValueUsd { get; set; }

    public int TotalTokens { get; set; }

    public int TotalWallets { get; set; }

    public ICollection<Chain> Chains { get; set; } = new List<Chain>(); 
}
