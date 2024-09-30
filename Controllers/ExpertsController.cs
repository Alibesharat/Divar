using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using divar.Services;
using divar.ViewModels;
using divar.DAL;
using divar.DAL.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace divar.Controllers;

/// <summary>
///Divar posts
/// </summary>
public class ExpertsController : Controller
{
    private readonly ILogger<ExpertsController> _logger;

    private readonly DivarDataContext _Db;

    private readonly IHttpContextAccessor _httpContextAccessor;



    public ExpertsController(ILogger<ExpertsController> logger , DivarDataContext db,IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _Db = db;
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("Login")]
    public  IActionResult Login()
    {
         return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public  async Task<IActionResult> Login(LoginViewModel model)
    {

        if (ModelState.IsValid)
        {
            var Expert =  _Db.Experts.FirstOrDefault(c=>c.PhoneNumber == model.PhoneNumber && c.Password == model.Password);
            if (Expert is not null)
            {
                //TODO :  sign-in
                 var claims = new List<Claim>
                    {
                        new(ClaimTypes.MobilePhone, Expert.PhoneNumber),
                         new(ClaimTypes.Name, Expert.FullName)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                     var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await _httpContextAccessor.HttpContext.SignInAsync("Cookies", claimsPrincipal, new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30) // Cookie expiration
                    });

                return RedirectToAction("Index", "Experts");
            }
            ModelState.AddModelError(string.Empty, "اطلاعات حساب کاربری صحیح نمی باشد");
        }
        return View(model);
    }

    public IActionResult AccessDenied()
    {
        return Ok("You are not access to this page ");
    }

    public async Task<IActionResult> Logout()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync("Cookies");
        return RedirectToAction("Index", "Home");
    }




   



   

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
