
using System.ComponentModel.DataAnnotations;
namespace KaiCryptoTracker.Models;

public class Coins
{
    [Key]
    public string CoinGeckoId { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public string? Symbol { get; set; } = string.Empty;
    public string? BinanceSymbol { get; set; }
}