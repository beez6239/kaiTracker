using System.ComponentModel.DataAnnotations; 
namespace KaiCryptoTracker.Models; 

public class User
{
    [Required]
     public Guid ID {get;set;}

     [Required]
     public string Name {get;set;} = string.Empty; 

     [Required]
     public string Surname {get;set;} = string.Empty; 

     [Required]
     public string Email {get;set;} = string.Empty; 

     [Required]
     public string Password {get;set;} = string.Empty; 
     
     public string? Country { get; set; }

}