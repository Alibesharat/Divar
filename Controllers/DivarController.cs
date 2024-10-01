
using divar.Services;
using divar.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace divar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DivarController : ControllerBase

    {

        public DivarService _divarService;

        public DivarController(DivarService divarService)
        {
            _divarService = divarService;
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
            string redirectUrl = $"https://f44f-204-18-233-125.ngrok-free.app/Inquiry/GetPostData";
             
            var postData = $"{userId}:{postToken}:{peerId}";
            string encodedPostData = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(postData));

            List<DivarScope> scopes =
            [
                DivarScope.CHAT_MESSAGE_SEND,
                DivarScope.USER_PHONE
            ];
            string generatedUrl =  _divarService.GenerateAuthorizationUrl(redirectUrl,postToken,encodedPostData);
            System.Console.WriteLine(generatedUrl);
            return Redirect(generatedUrl);
        }


        

    }
}