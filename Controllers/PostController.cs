using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using divar.Services;
using divar.ViewModels;

namespace divar.Controllers;

/// <summary>
///Divar posts
/// </summary>
public class PostController : Controller
{
    private readonly ILogger<PostController> _logger;
    private readonly DivarService _divarService;

    public PostController(ILogger<PostController> logger, DivarService divarService)
    {
        _logger = logger;
        _divarService = divarService;
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
        var _data=DateTime.UtcNow.AddDays(date);
        return Ok($"{name}-{mobile}-{option}-{date}");
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
