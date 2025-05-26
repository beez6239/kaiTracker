namespace KaiCryptoTracker.ApiModels;

public class WalletPnlSummary
{
    public int total_count_of_trades { get; set; }
    public string total_trade_volume { get; set; } = string.Empty;
    public string total_realized_profit_usd { get; set; } = string.Empty;
    public int total_realized_profit_percentage { get; set; }
    public int total_buys { get; set; }
    public int total_sells { get; set; }
    public string total_sold_volume_usd { get; set; } = string.Empty;
    public string total_bought_volume_usd { get; set; } = string.Empty;
    
}