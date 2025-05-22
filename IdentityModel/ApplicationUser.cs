using Microsoft.AspNetCore.Identity;

namespace KaiCryptoTracker.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
   
    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string? Country { get; set; }

    public ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
}