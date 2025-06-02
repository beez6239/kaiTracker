namespace KaiCryptoTracker.ApiModels;

public class TokenBalanceByWallet
{
    public string Cursor { get; set; }
    public int Page { get; set; }
    public int Page_Size { get; set; }

    public List<TokenResult> Result { get; set; } = new List<TokenResult>() { };
}

public class TokenResult
{
    public string Token_Address { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;
    public string Thumbnail { get; set; } = string.Empty;
    public string Decimals { get; set; } = string.Empty;
    public string Balance { get; set; } = string.Empty;
    public string Possible_Spam { get; set; } = string.Empty;
    public bool Verified_Contract { get; set; } 
    public string Balance_Formatted { get; set; } = string.Empty;
    public double Usd_Price { get; set; } 
    public double Usd_Price_24hr_Percent_Change { get; set; }
    public double Usd_Price_24hr_Usd_Change { get; set; }
    public double Usd_Value { get; set; }
    public double Usd_Value_24hr_Usd_Change { get; set; }
    public bool Native_Token { get; set; }
    public double Portfolio_Percentage { get; set; }
}
