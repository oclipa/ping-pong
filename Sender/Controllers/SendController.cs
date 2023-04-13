using Microsoft.AspNetCore.Mvc;
using Ports;

namespace Sender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendController : ControllerBase
    {
        private readonly IPinger _pinger;
        private readonly ILogger<SendController> _logger;

        public SendController(IPinger pinger, ILogger<SendController> logger)
        {
            _pinger = pinger;
            _logger = logger;
        }

        [HttpPost(Name = "Send")]
        public async Task<bool> Send(MessagePackage message)
        {
            _logger.LogInformation($"Received {message.Message}");

            await Task.Delay(1000);

            return await _pinger.PingAsync(new MessagePackage { Message = "ping" });
        }
    }
}
