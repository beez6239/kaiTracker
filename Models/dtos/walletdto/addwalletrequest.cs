
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
namespace KaiCryptoTracker.Models;

public class AddWalletRequest
{
    public string? WalletName { get; set; } = string.Empty;

    [Required]
    public string WalletAddress { get; set; }
}