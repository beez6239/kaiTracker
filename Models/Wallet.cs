using KaiCryptoTracker.Models;

public class Wallet
{
    public Guid WalletId { get; set; }

    //fk
    public Guid UserId { get; set; }
    public string? WalletName { get; set; } = string.Empty;
    public string? WalletAddress { get; set; } = string.Empty; 
    
    public User User { get; set; }

}