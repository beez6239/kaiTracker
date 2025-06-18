namespace KaiCryptoTracker.MarketDto;

public class MarketDataDto
{
    public string symbol { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public decimal current_price { get; set; } 
    public decimal market_cap { get; set; } 
    public decimal total_volume { get; set; } 
    public decimal high_24h { get; set; } 
    public decimal low_24h { get; set; } 
    public decimal price_change_24h  { get; set; }
    public decimal ath { get; set; }  
    public string image { get; set; } = string.Empty; 
    public DateTime last_updated { get; set; } 
}