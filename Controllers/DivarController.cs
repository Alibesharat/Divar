
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
        public async Task<IActionResult> StartChat([FromBody] DivarRequest divarRequest)
        {
            Console.WriteLine("Divar is here ... ");

            //Handle the incoming data
            if (divarRequest == null)
            {
                return BadRequest("Invalid data received");
            }

            // Example: L
            var userId = divarRequest.user_id; // The user who requested app .
            var peerId = divarRequest.peer_id; //The other user in the chat .
            var postToken = divarRequest.post_token; // the ads token .

            var PostData = await _divarService.GetPostDataAsync(postToken);
            Console.WriteLine(PostData.Data.Title);

            var response = new
            {
                status = "200",
                message = "success",
                url = $"https://sharifexperts.ir/Inquiry/{postToken}"
            };

            return Ok(response);
        }

    }
}