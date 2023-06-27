using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DemoAppSignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHubContext<MessageHub> _hubContext;
        public HomeController(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public IActionResult SendMessage(string user, string message)
        {
            _hubContext.Clients.All.SendAsync("ReceiveMessage", user, message);
            return Ok();
        }
        public IActionResult Index()
        {
            return Ok();
        }

    }
}
