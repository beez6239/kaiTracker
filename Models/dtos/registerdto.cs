using System.ComponentModel.DataAnnotations;
using KaiCryptoTracker.CustomModelValidator; 
namespace KaiCryptoTracker.Models; 

public class UserDto
{
    [Required(ErrorMessage = "Name can't be empty")]
     public string Name {get;set;} = string.Empty; 

     [Required(ErrorMessage = "Surname can't be empty")]
     public string Surname {get;set;} = string.Empty;

    [Required(ErrorMessage = "Email requird")]
     [DataType(DataType.EmailAddress)]
     public string Email { get; set; } = string.Empty;
      

     [Required(ErrorMessage = "Password required")]
     [DataType(DataType.Password)]
     public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please confirm password")]
    [DataType(DataType.Password)]
    [ConfirmPasswordValidator("Password")]
     public string ConfirmPassword { get; set; } = string.Empty;


    public string? Country { get; set; } = string.Empty;

}