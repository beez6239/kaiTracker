using System.ComponentModel.DataAnnotations.Schema;
using KaiCryptoTracker.Identity;

namespace KaiCryptoTracker.Models;

public class Portfolio
{
    public Guid PortfolioId { get; set; }

    public string PortfolioName { get; set; } = string.Empty;
    public decimal TotalValueUsd { get; set; }

    public int TotalTokens { get; set; }

    //fk 
    public Guid UserId { get; set; }
    public ApplicationUser? user { get; set; }

    public int TotalWallets { get; set; }

    public ICollection<Chain> Chains { get; set; } = new List<Chain>();

    public ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
}
