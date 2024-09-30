using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using divar.ViewModels;
using divar.DAL;
using divar.DAL.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using divar.Extensions;
using Microsoft.EntityFrameworkCore;

namespace divar.Controllers;

/// <summary>
///Divar posts
/// </summary>
[Route("Experts")]
[Authorize]
public class ExpertsController : Controller
{
    private readonly ILogger<ExpertsController> _logger;

    private readonly DivarDataContext _Db;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public ExpertsController(ILogger<ExpertsController> logger, DivarDataContext db, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _Db = db;
        _httpContextAccessor = httpContextAccessor;
    }


    [HttpGet(nameof(Index))]


    public IActionResult Index()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized();
        }
        var expert = _Db.Experts.Include(c => c.Reservations).FirstOrDefault(c => c.PhoneNumber == User.GetPhoneNumber());
        return View(expert);
    }

    [HttpGet(nameof(Login))]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost(nameof(Login))]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]

    public async Task<IActionResult> Login(LoginViewModel model)
    {

        if (ModelState.IsValid)
        {
            var Expert = _Db.Experts.FirstOrDefault(c => c.PhoneNumber == model.PhoneNumber && c.Password == model.Password);
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


    [HttpPost("UpdateReservation")]
    public IActionResult UpdateReservation([FromBody] UpdateReservationRequest request)
    {
        var reservation = _Db.Reservations.Find(request.Id);
        if (reservation is null)
        {
            return NotFound("Reservation not found.");
        }

        var expert = _Db.Experts.FirstOrDefault(c => c.PhoneNumber == User.GetPhoneNumber());
        if (expert is null || expert.ExpertId != reservation.ExpertId)
        {
            return NotFound("Expert not found or not authorized to update this reservation.");
        }

        // Validate reviewStatus against enum
        if (!Enum.IsDefined(typeof(ReviewStatus), request.ReviewStatus))
        {
            return BadRequest("Invalid review status.");
        }

        reservation.ReviewStatus = (ReviewStatus)request.ReviewStatus;
        reservation.ExpertReviewResult = request.ExpertReviewResult;

        _Db.Reservations.Update(reservation);
        _Db.SaveChanges();

        return Ok(); // Optionally return the updated reservation
    }

    public class UpdateReservationRequest
    {
        public int Id { get; set; }
        public int ReviewStatus { get; set; }
        public string ExpertReviewResult { get; set; }
    }

    [HttpGet(nameof(Logout))]
    public async Task<IActionResult> Logout()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync("Cookies");
        return RedirectToAction("Index", "Home");
    }













}
