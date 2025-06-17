namespace KaiCryptoTracker.MarketDto;

public class MarketDataDto
{
    public string symbol { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public decimal current_price { get; set; } 
    public long market_cap { get; set; } 
    public string total_volume { get; set; } = string.Empty;
    public string high_24h { get; set; } = string.Empty;
    public string low_24h { get; set; } = string.Empty;
    public int price_change_24  { get; set; } 
    public string ath { get; set; } 
    public DateTime last_updated { get; set; } 
}