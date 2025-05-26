using System.ComponentModel.DataAnnotations.Schema; 

namespace KaiCryptoTracker.Models;

public class TokenMetadata
{
    
    public Guid TokenMetadataId { get; set; }

    //fk 
    public Guid ChainId { get; set; }
    public Chain Chain { get; set; }


    public string Address { get; set; } = string.Empty;

   
    public string Symbol { get; set; } = string.Empty;

   
    public string Name { get; set; } = string.Empty;
    
    public int Decimals { get; set; }
    
    [Column(TypeName = "decimal(38, 18)")]
    
    public bool VerifiedContract { get; set; }   

    public ICollection<TokenHolding> TokenHoldings = new List<TokenHolding>();

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