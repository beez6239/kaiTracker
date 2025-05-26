
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Identity;

namespace KaiCryptoTracker.CustomModelValidator; 

public class ConfirmPasswordValidator: ValidationAttribute
{
    public string Password {get; set;} 
    public ConfirmPasswordValidator(string password )
    {
      Password = password; 
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
       if(value != null)
       {
          
          string? ConfirmPassword = value.ToString();

            if(Password != null)
            {
                //Get the Password property
              PropertyInfo? property = validationContext.ObjectType.GetProperty(Password);
              //Get the value from the property 
             string? FirstPassword = Convert.ToString(property?.GetValue(validationContext.ObjectInstance));

             
                 if (FirstPassword != null)

                    if (ConfirmPassword != FirstPassword)
                    {
                        return new ValidationResult("Password must match");
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }

            }           
           
       }
       return null;

    }
}