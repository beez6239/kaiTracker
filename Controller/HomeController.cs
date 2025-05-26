
using Microsoft.AspNetCore.Mvc;
using KaiCryptoTracker.Models;
using KaiCryptoTracker.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace KaiCryptoTracker.Controllers;

public class HomeController : Controller
{
  private ILogger<HomeController> _logger;
  private readonly UserManager<ApplicationUser> _userManager;
  public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
  {
    _logger = logger;
    _userManager = userManager;
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
  public IActionResult TestPage()
  {
    return View();
  }


}