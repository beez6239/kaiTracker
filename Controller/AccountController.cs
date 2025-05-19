
using Microsoft.AspNetCore.Mvc;
using KaiCryptoTracker.Models;

namespace KaiCryptoTracker.Controllers;

public class AccountController : Controller
{
   private ILogger<AccountController> _logger;
   public AccountController(ILogger<AccountController> logger)
   {
      _logger = logger;
   }

   [Route("[action]")]
   public IActionResult Login()
   {
      return View();
   }


   [Route("[action]")]
   [HttpPost]
   public IActionResult Login(LoginDto login)
   {
      return View();
   }
    
   //  [Route("[action]")]
   // public IActionResult Signup()
   // {
   //    return View(); 
   // }


   //  [HttpPost]
   //  [Route("[action]")]
   // public IActionResult Signup(string test)
   // {
   //    return View(); 
   // }

}