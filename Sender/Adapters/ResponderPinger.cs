using Newtonsoft.Json;
using Sender.Controllers;
using Sender.Ports;
using System.Net.Http.Headers;

namespace Sender.Adapters
{
    public class ResponderPinger : IPinger
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ResponderPinger(IHttpClientFactory factory)
        {
            _httpClientFactory = factory;
        }

        public async Task<bool> PingAsync(MessagePackage message)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("http://webapptest-responder-1:5001");

            var myContent = JsonConvert.SerializeObject(message);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //var stringContent = new StringContent(message);


            bool status = false;
            HttpResponseMessage response = await httpClient.PostAsync("/api/Respond", byteContent);
            if (response.IsSuccessStatusCode)
            {
                status = await response.Content.ReadAsAsync<bool>();
            }
            return status;
        }
    }
}
