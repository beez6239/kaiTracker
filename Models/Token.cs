using System.ComponentModel.DataAnnotations; 

namespace KaiCryptoTracker.Models;

public class TokenDetails
{
    [Required]
    public Guid TokenId { get; set; }

    //fk 
    public Guid ChainId { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string Symbol { get; set; }

    [Required]
    public string Name { get; set; }
    [Required]
    public int Decimals { get; set; }
    [Required]
    public string Balance { get; set; }
    [Required]
    public bool Verified_Contract { get; set; }
    
    public Chain Chain { get; set; }

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