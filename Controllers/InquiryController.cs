using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using divar.Services;
using divar.ViewModels;
using divar.DAL;
using divar.DAL.Models;
using Microsoft.Extensions.Options;

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

    private readonly DivarSetting _divarSetting;

    public InquiryController(ILogger<InquiryController> logger,IOptions<DivarSetting> divarSetting,DivarService divarService)
    {
        _logger = logger;
        _divarSetting = divarSetting.Value;
       _divarService = divarService;
       

       
       // _Db.Initialize();
    }



     [HttpGet("GetPostData")]
    public async Task<IActionResult> GetPostDataAsync(string scope,string state)
    {
       
        _logger.LogInformation($"state is {state}");
        _logger.LogInformation($"scope is {scope}");


       
        string postToken = "state";
        PostData postData;
        if (!string.IsNullOrEmpty(postToken))
        {
            var accessToken = await _divarService.ExchangeCodeForAccessTokenAsync("code",_divarSetting.RedirectUrl);
             Console.WriteLine("AccessToken",accessToken);
             postData = await _divarService.GetPostDataAsync(postToken,accessToken);
             if(postData == null) throw new ArgumentNullException("Post Data is null or can not handshake with divar");
        }
        else
        {
             postData = new PostData()
            {
                Token = "new",
                Data = new Data()
                {
                    Title = "درخواست جدید",
                }
            };
        }
         return View(postData);
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
