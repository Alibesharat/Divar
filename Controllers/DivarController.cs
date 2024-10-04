
using divar.Services;
using divar.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace divar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DivarController : ControllerBase

    {

        public DivarService _divarService;
        private readonly DivarSetting _divarSetting;

        public DivarController(DivarService divarService, IOptions<DivarSetting> divarSetting)
        {
            _divarService = divarService;
            _divarSetting = divarSetting.Value;

        }


        [HttpPost(nameof(StartChat))]
        public IActionResult StartChat([FromBody] DivarRequest divarRequest)
        {
            Console.WriteLine("Divar is here ... ");
            //Handle the incoming data
            if (divarRequest == null)
            {
                return BadRequest("Invalid data received");
            }

            var userId = divarRequest.user_id; // The user who requested app .
            var peerId = divarRequest.peer_id; //The other user in the chat .
            var postToken = divarRequest.post_token; // the ads token .

            var postData = $"{userId}:{postToken}:{peerId}";
            string encodedPostData = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(postData));

           
            string generatedUrl = _divarService.GenerateAuthorizationUrl(postToken);
            System.Console.WriteLine(generatedUrl);
            return Redirect(generatedUrl);
        }




    }
}