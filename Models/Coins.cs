
using System.ComponentModel.DataAnnotations;
namespace KaiCryptoTracker.Models;

public class Coins
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string symbol { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
}