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

    private readonly SmsService _sms;

    public InquiryController(ILogger<InquiryController> logger, DivarService divarService, DivarDataContext db,SmsService sms)
    {
        _logger = logger;
        _divarService = divarService;
        _Db = db;
        _sms =sms;
        _Db.Initialize();
    }

    [HttpGet("Inquiry/{postToken?}")]
    public async Task<IActionResult> Index(string postToken = null)
    {
        if (!string.IsNullOrEmpty(postToken))
        {
            var postData = await _divarService.GetPostDataAsync(postToken);
            System.Console.WriteLine(postData.Token);
          
            return View(postData);
        }else
        {
              var postData = new PostData()
            {
                Data = new Data()
                {
                    Title = "درخواست جدید"
                }
            };
             return View(postData);
        }
    }


    public  IActionResult Reservation([FromForm] string name, string mobile, int option, int date)
    {
        var _date = DateTime.UtcNow.AddDays(date);

        var reservation = new Reservation()
        {
            BookTime = _date,
            ExpertOption = (ExpertOption)option,
            FullName = name,
        };
        _Db.Reservations.Add(reservation);

        _Db.SaveChanges();
          string expertName = "آرمین مسعودی";
          string expertNumber = "09190078747";
          string message = $"{name} عزیز درخواست وقت مشاوره شما با موفقیت انجام شد . کارشناس آرمین مسعودی جهت هماهنگی های بیشتر با شما تماس میگیرد";
         _sms.SendMessageToCustomer(name,expertName,mobile);
         _sms.SendMessageExpert(name,expertName,expertNumber);
          

         ViewBag.message = message;
       
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
