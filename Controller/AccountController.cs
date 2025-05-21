
using Microsoft.AspNetCore.Mvc;
using KaiCryptoTracker.Models;
using KaiCryptoTracker.TokenService;

namespace KaiCryptoTracker.Controllers;

public class AccountController : Controller
{
   private ILogger<AccountController> _logger;
   private readonly ITokenService _tokenService;
   public AccountController(ILogger<AccountController> logger, ITokenService tokenService)
   {
      _tokenService = tokenService;
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


   [Route("[action]")]
   public IActionResult Dashboard()
   {
      return View();
   }

   [Route("[action]")]
   public IActionResult AddWallet()
   {
      return View();
   }

   [Route("[action]")]
   [HttpPost]
   public IActionResult AddWallet(AddWalletRequest addWalletRequest)
   {

      // _tokenService.GetTokenBalanceAsync(address, chain);

      return Ok();
   }

}