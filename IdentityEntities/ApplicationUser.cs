using Microsoft.AspNetCore.Identity;
using KaiCryptoTracker.Models;

namespace KaiCryptoTracker.Identity;

public class ApplicationUser : IdentityUser<Guid>
{

    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string? Country { get; set; }

    public ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();

    public ICollection<Portfolio>? Portfolios { get; set; } = new List<Portfolio>();
}