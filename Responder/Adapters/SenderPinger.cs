using Newtonsoft.Json;
using Responder.Controllers;
using Responder.Ports;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Responder.Adapters
{
    public class SenderPinger : IPinger
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SenderPinger(IHttpClientFactory factory) 
        {
            _httpClientFactory = factory;
        }

        public async Task<bool> PingAsync(MessagePackage message)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("http://webapptest-sender-1:5000");

            var myContent = JsonConvert.SerializeObject(message);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //var stringContent = new StringContent(message);

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
