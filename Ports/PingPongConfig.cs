namespace Ports
{
    public class PingPongConfig
    {
        public const string PingPing = "PingPong";

        public string AppName { get; set; } = String.Empty;
        public string TargetName { get; set; } = String.Empty;
        public string TargetPort { get; set; } = String.Empty;
    }
}
