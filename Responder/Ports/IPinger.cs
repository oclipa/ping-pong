using Responder.Controllers;

namespace Responder.Ports
{
    public interface IPinger
    {
        Task<bool> PingAsync(MessagePackage message);
    }
}
