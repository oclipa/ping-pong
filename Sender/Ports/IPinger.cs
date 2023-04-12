using Sender.Controllers;

namespace Sender.Ports
{
    public interface IPinger
    {
        Task<bool> PingAsync(MessagePackage message);
    }
}
