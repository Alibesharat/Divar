using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using divar.Services;
using divar.ViewModels;

namespace divar.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DivarService _divarService;

    public HomeController(ILogger<HomeController> logger, DivarService divarService)
    {
        _logger = logger;
        _divarService = divarService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
