using System.ComponentModel.DataAnnotations; 

namespace KaiCryptoTracker.Models;

public class TokenDetails
{
    public Guid ID { get; set; }

    [Required]
    public string Address { get; set; }
    public string Symbol { get; set; }
    public string Name { get; set; }
    public int Decimals { get; set; }
    public string Chain { get; set; }
    public string RawBalance { get; set; }
    public bool Verified_Contract { get; set; }
    public decimal? PriceUsd { get; set; }

}

public class Market
{
    //  public Guid ID {get;set;}
    //  public string Name {get;set;}
    //  public string Symbol {get;set;}
    //  public string Chain {get;set;}
    //  public string Address {get;set;}
    //  public int Amount {get; set;}

}

public class Wallet
{
    public Guid WalletID { get; set; }
    public string? WalletName { get; set; } = string.Empty; 
    public string? WalletAddress { get; set; }

}