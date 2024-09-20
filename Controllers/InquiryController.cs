using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using divar.Services;
using divar.ViewModels;
using divar.DAL;
using divar.DAL.Models;

namespace divar.Controllers;

/// <summary>
///Divar posts
/// </summary>
public class InquiryController : Controller
{
    private readonly ILogger<InquiryController> _logger;
    private readonly DivarService _divarService;

    private readonly DivarDataContext _Db;

    public InquiryController(ILogger<InquiryController> logger, DivarService divarService,DivarDataContext db)
    {
        _logger = logger;
        _divarService = divarService;
        _Db = db;
    }

    [HttpGet("Post/{postToken}")]
    public async Task<IActionResult> Index(string postToken)
    {
        if (string.IsNullOrEmpty(postToken))
        {
            return BadRequest("postToken is required");
        }

        try
        {
            var postData = await _divarService.GetPostDataAsync(postToken);

            // if (postData == null)
            // {
            //     return NotFound("Post data not found");
            // }
            postData = new PostData()
            {
                Data = new Data()
                {
                    Title = "پرینتر canon"
                }
            };
            return View(postData);
        }
        catch (Exception ex)
        {
            // Log the exception (ex) if necessary
            return StatusCode(500, "متاسفانه خطایی رخ داد");
        }
    }


    public IActionResult Reservation([FromForm] string name,string mobile,int option,int date)
    {
        var _date=DateTime.UtcNow.AddDays(date);

         var reservation = new Reservation()
         {
            BookTime = _date,
            ExpertOption = (ExpertOption)option,
            FullName = name,
         };
        _Db.Reservations.Add(reservation);

        _Db.SaveChanges();

        ViewBag.message  = $"{name} عزیز کارشناس آرمین مسعودی جهت هماهنگی های بیشتر با شما تماس میگیرد";
        return View("result");
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
