
using Microsoft.AspNetCore.Mvc;
using KaiCryptoTracker.Models; 

namespace KaiCryptoTracker.Controllers;

public class HomeController : Controller
{
  private ILogger<HomeController> _logger;
  public HomeController(ILogger<HomeController> logger)
  {
    _logger = logger;
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
  public IActionResult Register(User user)
  {
    return View();
  }


}