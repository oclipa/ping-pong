using Microsoft.AspNetCore.Mvc;
using Ports;

namespace Responder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespondController : ControllerBase
    {
        private readonly IPinger _pinger;
        private readonly ILogger<RespondController> _logger;

        public RespondController(IPinger pinger, ILogger<RespondController> logger)
        {
            _pinger = pinger;
            _logger = logger;
        }

        [HttpPost(Name = "Respond")]
        public async Task<bool> Respond(MessagePackage message)
        {
            _logger.LogInformation($"Received {message.Message}");

            await Task.Delay(1000);
            
            return await _pinger.PingAsync(new MessagePackage { Message = "pong"});
        }
    }
}
