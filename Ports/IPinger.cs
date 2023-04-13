namespace Ports
{
    public interface IPinger
    {
        Task<bool> PingAsync(MessagePackage message);
    }
}
