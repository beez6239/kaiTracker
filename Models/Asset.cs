using System.ComponentModel.DataAnnotations; 

namespace KaiCryptoTracker.Models; 

public class Asset
{
     public Guid ID {get;set;}

     [Required]
     public string Name {get;set;} = string.Empty; 
     public string Symbol {get;set;} = string.Empty; 
     public string Chain {get;set;} = string.Empty; 
     public string Address {get;set;} = string.Empty; 
     public int Amount {get; set;} 

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