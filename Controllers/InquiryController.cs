using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using divar.Services;
using divar.ViewModels;
using divar.DAL;
using divar.DAL.Models;

namespace divar.Controllers;

/// <summary>
///InquiryController
/// </summary>
[Route("Inquiry")]
public class InquiryController : Controller
{
    private readonly ILogger<InquiryController> _logger;
    private readonly DivarService _divarService;

    private readonly DivarDataContext _Db;

    private readonly SmsService _sms;

    public InquiryController(ILogger<InquiryController> logger)
    {
        _logger = logger;
       
       // _Db.Initialize();
    }



     [HttpGet("GetPostData")]
    public IActionResult GetPostData()
    {
       
        // System.Console.WriteLine($"state is {state}");
        // System.Console.WriteLine($"code is {code}");
         var queryParams = HttpContext.Request.Query;

            // Log query parameters
            foreach (var param in queryParams)
            {
                Console.WriteLine($"Query Parameter: {param.Key} = {param.Value}");
            }

        return Ok($"Done !");
        // string postToken = "state";
        // PostData postData;
        // if (!string.IsNullOrEmpty(postToken))
        // {
        //     var accessToken = await _divarService.ExchangeCodeForAccessTokenAsync("code","https://1163-2-176-95-255.ngrok-free.app/Inquiry/GetPostData");
        //     System.Console.WriteLine("AccessToken",accessToken);
        //      postData = await _divarService.GetPostDataAsync(postToken,accessToken);
        //      if(postData == null) throw new ArgumentNullException("Post Data is null or can not handshake with divar");
        // }
        // else
        // {
        //      postData = new PostData()
        //     {
        //         Token = "new",
        //         Data = new Data()
        //         {
        //             Title = "درخواست جدید",
        //         }
        //     };
        // }
        //  return View(postData);
    }

    [HttpGet(nameof(Reservation))]
    public  IActionResult Reservation([FromForm]string postTitle, string name, string mobile, int option, int date,string postToken)
    {
        var _date = DateTime.UtcNow.AddDays(date);

        var expert = _Db.Experts.Find(1);

        var reservation = new Reservation()
        {
            PostTitle = postTitle,
            BookTime = _date,
            ExpertOption = (ExpertOption)option,
            FullName = name,
            PostToken = postToken,
            ExpertId = expert.ExpertId,
            Expert = expert,
            PhoneNumber = mobile,
            TrackingCode = GenerateTackingCode(),
            ReviewStatus = ReviewStatus.ExpertAssigned
        };
        _Db.Reservations.Add(reservation);

        _Db.SaveChanges();
         
         string message = $"{reservation.FullName} عزیز  سفارش شما با کد رهگیری  {reservation.TrackingCode} با موفقیت ثبت شد ، کارشناس {expert.FullName} جهت هماهنگی بیشتر با شما تماس میگیرد . ";
         _sms.SendMessageToCustomer(name,expert.FullName,mobile);
         _sms.SendMessageExpert(name,expert.FullName,expert.PhoneNumber);

         if(postToken !="new")
         {
            return Redirect($"https://divar.ir");
         }
          
         ViewBag.message = message;
         return View("result");
    }

    private string GenerateTackingCode()
    {
        Random r = new Random(); 
        int randomNumber = r.Next(1000, 10000);
        string trackingCode = $"sh-{randomNumber}";
        return trackingCode;

    }
    
    

   
}
