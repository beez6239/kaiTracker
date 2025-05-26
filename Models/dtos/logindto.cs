using System.ComponentModel.DataAnnotations; 
namespace KaiCryptoTracker.Models;

public class LoginDto
{
     [Required(ErrorMessage = "Username/Email can't be empty")]
     public string Email { get; set; } = string.Empty; 

     [Required(ErrorMessage = "Password Required")]
     public string Password {get;set;} = string.Empty; 

  

}