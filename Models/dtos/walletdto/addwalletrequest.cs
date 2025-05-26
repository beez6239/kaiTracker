
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
namespace KaiCryptoTracker.Models;

public class AddWalletRequest
{
    
    [Required(ErrorMessage = "Wallet name can't be emtpy")]
    public string? WalletName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Wallet address can't be empty")]
    public string WalletAddress { get; set; } = string.Empty;

}