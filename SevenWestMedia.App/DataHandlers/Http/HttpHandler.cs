using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SevenWestMedia.App.DataHandlers.Interfaces.Http;

namespace SevenWestMedia.App.DataHandlers.Http
{
    public class HttpHandler<T> : IHttpHandler<T>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        public HttpHandler(IHttpClientFactory httpClientFactory, ILogger<HttpHandler<T>> logger)
        {
            this._httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IEnumerable<T>> GetAsync(string url)
        {
            try
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                HttpClient httpClient = _httpClientFactory.CreateClient();
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string contentString = await httpResponseMessage.Content.ReadAsStringAsync();
                    IEnumerable<T> result = JsonConvert.DeserializeObject<IEnumerable<T>>(contentString);
                    return result;
                }
            }
            catch (JsonException ex)
            {
                _logger.LogError($"Invalid JSON format in HTTP response. Message: {ex.Message}");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Not able to GET data. Message: {ex.Message}");
            }
            return default;
        }
    }
}