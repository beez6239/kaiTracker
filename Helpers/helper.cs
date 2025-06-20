namespace KaiCryptoTracker.Helpers;


//format number to 4 decimals without rounding
public static class HelperClass
{
    public static decimal FormatDigitToFourDecimalHelper(decimal number) => Math.Truncate(number * 10000m) / 10000m;


    //helper method to help map input of type int  to enum 
    public static CandleInterval InputIntervals(int number)
    {
        return number switch
        {
            3 => CandleInterval.ThreeMinutes,
            5 => CandleInterval.FiveMinutes,
            15 => CandleInterval.FifteenMinutes,
            30 => CandleInterval.ThirtyMinutes,

            _ => throw new ArgumentException($"{0} is invalid/not supported yet", nameof(number))
        };
    }
    public static string[] AlertTypeValues()
    {
        return ["price", "ma", "Rsi"];
    }

    public static string FormatCoinSymbol(string coin)
    {
        return $"{coin}USDT"; 
    }

}

