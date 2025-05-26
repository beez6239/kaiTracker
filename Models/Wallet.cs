
using KaiCryptoTracker.Identity;

using Microsoft.AspNetCore.Identity;

namespace KaiCryptoTracker.Models;

public class Wallet
{

    public Guid WalletId { get; set; }

    //fk
    public Guid UserId { get; set; }
    public ApplicationUser? User { get; set; } //for navigation 

    //fk
    public Guid PortfolioId { get; set; }
    public Portfolio? Portfolio { get; set; } //for navigation 
    
    public string WalletName { get; set; } = string.Empty;
    public string WalletAddress { get; set; } = string.Empty;

    public ICollection<TokenHolding> tokenHoldings { get; set; } = new List<TokenHolding>();

}