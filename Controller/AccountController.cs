
using Microsoft.AspNetCore.Mvc;
using KaiCryptoTracker.Models;
using KaiCryptoTracker.TokenService;
using Microsoft.AspNetCore.Identity;
using KaiCryptoTracker.Identity;
using KaiCryptoTracker.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using KaiCryptoTracker.WalletService;
using KaiCryptoTracker.PortfolioService;
using KaiCryptoTracker.AllApiCalls;
using Newtonsoft.Json;
using KaiCryptoTracker.AlertModel;
using KaiCryptoTracker.ApiModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using KaiCryptoTracker.Helpers;

namespace KaiCryptoTracker.Controllers;

public class AccountController : Controller
{
   private ILogger<AccountController> _logger;
   private readonly SignInManager<ApplicationUser> _signInManager;
   private readonly UserManager<ApplicationUser> _userManager;
   private readonly ApplicationDbContext _dbcontext;
   private readonly ITokenService _tokenService;
   private readonly IWalletService _walletservice;
   private readonly IPortfolioService _portfolioservice;
   

   public AccountController(ILogger<AccountController> logger, ITokenService tokenService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ApplicationDbContext dbcontext, IWalletService walletservice, IPortfolioService portfolioservice)
   {
      _tokenService = tokenService;
      _signInManager = signInManager;
      _userManager = userManager;
      _dbcontext = dbcontext;
      _walletservice = walletservice;
      _portfolioservice = portfolioservice;

      _logger = logger;

   }

   [Route("[action]")]
   public IActionResult Login()
   {
      return View();
   }


   [Route("[action]")]
   [HttpPost]
   public async Task<IActionResult> Login(LoginDto login)
   {
      if (ModelState.IsValid)
      {
         var user = await _userManager.FindByNameAsync(login.Email);
         if (user != null)
         {
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
            if (result.Succeeded)
            {
               await _signInManager.SignInAsync(user, false, null);

               return RedirectToAction("Portfolio");
            }
         }
      }
      return View();
   }


   [Route("[action]")]
   public async Task<IActionResult> Portfolio()
   {
      // var user = await _userManager.GetUserAsync(User);

      // if (user != null)
      // {
      //    var porfolioresult = await _dbcontext.Portfolios
      //    .Include(p => p.Chains)
      //    .Include(w => w.Wallets)
      //    .FirstOrDefaultAsync(p => p.UserId == user.Id);


      //    if (porfolioresult == null)
      //    {
      //       porfolioresult = await _portfolioservice.AddPortfolioAsync(user.Id, null);
      //    }

      //    return View(porfolioresult);
      // }

      // ViewBag.Title = "Portflio Overview";

      return View();

   }

   [Route("[action]")]
   public IActionResult AddWallet()
   {
      return View();
   }

   [Route("[action]")]
   [HttpPost]
   public async Task<IActionResult> AddWallet(AddWalletRequest addWalletRequest)
   {
      if (ModelState.IsValid)
      {
         // var user = await _userManager.GetUserAsync(User);
         // if (user != null)
         // {

         // await _walletservice.AddWalletAsync(user.Id,addWalletRequest.WalletAddress, addWalletRequest.WalletChain, addWalletRequest.WalletName);    
         // }
      }

      return RedirectToAction("Dashboard", "Account");
   }


   [HttpPost]
   [Route("[action]")]
   public IActionResult AlertAction(AlertAction alert)
   {


      if (ModelState.IsValid)
      {
         var alertypes = AlertTypes.AlertTypeValues();
         if (alert.AlertType == alertypes[0]) //price alert 
         {

         } else if (alert.AlertType == alertypes[1]) //moving average alert
         {

         } else if (alert.AlertType == alertypes[2]) //rsi alert
         {

         }

         
   
      }
      return new JsonResult("");
   }


   [Route("[action]")]
    [HttpPost]
   public async Task<IActionResult> TestEndpoint()
   {

       var interval = HelperClass.InputIntervals(3);

       await _tokenService.GetCoinCandleDataAsync("BTCUSDT", interval);

      // Coins[]? data = JsonConvert.DeserializeObject<Coins[]>(result);

      return Ok();
   }


}