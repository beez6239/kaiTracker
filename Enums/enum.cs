public enum CandleInterval
{
    ThreeMinutes,
    FiveMinutes,
    FifteenMinutes,
    ThirtyMinutes

}


//Extention method to map enum to binance string 

public static class CandleIntervalExtension
{
    public static string ConvertBinanceString(this CandleInterval interval)
    {
        return interval switch
        {
            CandleInterval.ThreeMinutes => "1m",
            CandleInterval.FiveMinutes => "5m",
            CandleInterval.FifteenMinutes => "15m",
            CandleInterval.ThirtyMinutes => "5m",
            _ => throw new ArgumentNullException("invalid interval ", nameof(interval))

        };
    }
}