using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using divar.ViewModels;

namespace divar.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger )
    {
        _logger = logger;
        
    }
    public IActionResult Index()
    {
        return View();
    }


    public IActionResult AboutUs()
    {
        return View();
    }

    public IActionResult ContactUs()
    {
        return View();
    }

    public IActionResult KnowledgeBase()
    {
        return View();
    }

    public IActionResult Terms()
    {
        return View();
    }


    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult AccessDenied()
    {
        return Ok("You are not access to this page ");
    }


   
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
