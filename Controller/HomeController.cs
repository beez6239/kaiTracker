
using Microsoft.AspNetCore.Mvc;
using KaiCryptoTracker.Models;
using KaiCryptoTracker.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using KaiCryptoTracker.Market;

namespace KaiCryptoTracker.Controllers;

public class HomeController : Controller
{
  private ILogger<HomeController> _logger;
  private readonly MarketBase _market; 
  private readonly UserManager<ApplicationUser> _userManager;
  public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, MarketBase market)
  {
    _logger = logger;
    _userManager = userManager;
    _market = market; 
  }

  [Route("[action]")]
  [Route("/")]
  public IActionResult Home()
  {
    return View();
  }

  [Route("[action]")]
  public IActionResult About()
  {
    return View();
  }

  [Route("[action]")]
  public IActionResult Privacy()
  {
    return View();
  }

  [Route("[action]")]
  public IActionResult Register()
  {
    return View();
  }


  [Route("[action]")]
  [HttpPost]
  public async Task<IActionResult> Register(UserDto userdto)
  {
    if (ModelState.IsValid)
    {
      var newuser = new ApplicationUser()
      {
        UserName = userdto.Email,
        Name = userdto.Name,
        Surname = userdto.Surname,
        Email = userdto.Email,
        Country = userdto.Country

      };

      var addUserResponse = await _userManager.CreateAsync(newuser, userdto.ConfirmPassword);

      if (addUserResponse.Succeeded)
      {
         return RedirectToAction("Login", "Account");
      }
      else
      {
        foreach (var error in addUserResponse.Errors)
        {
          ModelState.AddModelError("RegisterError", error.Description);
        }
      }

    }
    return View();
  }
  

  [Route("[action]")]
  public async Task<IActionResult> Market(int page = 1)
  {
    int pagesize = 15; 
    var market =  await _market.livemarketdata();

    //get pagesize to display on each page
    var pagedcoins = market.Skip((page - 1) * pagesize).Take(pagesize).ToList();

    //save details in viewbag
    ViewBag.CurrentPage = page;
    ViewBag.PageSize = pagesize;
    ViewBag.TotalCoinsToDisplay = market.Count();

    
    return View(pagedcoins);
  }


}