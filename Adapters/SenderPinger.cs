using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ports;
using System.Net.Http.Headers;

namespace Adapters
{
    public class SenderPinger : IPinger
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _senderUri;

        public SenderPinger(IConfiguration configuration, IHttpClientFactory factory, ILogger<SenderPinger> logger) 
        {
            _httpClientFactory = factory;

            var pingPongConfig = new PingPongConfig();
            configuration.GetSection(PingPongConfig.PingPing).Bind(pingPongConfig);
            _senderUri = $"http://{pingPongConfig.AppName}-{pingPongConfig.TargetName}-1:{pingPongConfig.TargetPort}";
            logger.LogInformation($"Target: {_senderUri}");
        }

        public async Task<bool> PingAsync(MessagePackage message)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_senderUri);

            var myContent = JsonConvert.SerializeObject(message);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            bool status = false;
            HttpResponseMessage response = await httpClient.PostAsync("/api/Send", byteContent);
            if (response.IsSuccessStatusCode)
            {
                status = await response.Content.ReadAsAsync<bool>();
            }
            return status;
        }
    }
}
