using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Dapr.Client;
using DaprQueryParamArray.Models;

namespace DaprQueryParamArray.Services
{
    public class RequestService : IRequestService
    {
        private readonly DaprClient _daprClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public RequestService(DaprClient daprClient, IHttpClientFactory httpClientFactory)
        {
            _daprClient = daprClient;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDto> DaprMethodInvocation()
        {
            var queryParams = "name=test&name=test2&name=3";

            var httpRequest = _daprClient.CreateInvokeMethodRequest(HttpMethod.Get, "queryparams", "api/callee");
            var baseUri = new UriBuilder(httpRequest.RequestUri) {Query = queryParams};
            httpRequest.RequestUri = baseUri.Uri;

            var response = await _daprClient.InvokeMethodAsync<ResponseDto>(httpRequest);
            return response;
        }

        public async Task<ResponseDto> HttpInvocation()
        {
            using var httpClient = _httpClientFactory.CreateClient();
            var uri = "http://localhost:5000/api/Callee?name=test&name=test2&name=test3";

            using var request = new HttpRequestMessage(HttpMethod.Get, uri);

            var response = await httpClient.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ResponseDto>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<ResponseDto> HttpDaprInvocation()
        {
            using var httpClient = _httpClientFactory.CreateClient();
            var uri = "http://localhost:3500/v1.0/invoke/queryparams/method/api/callee?name=test&name=test2&name=test3";

            using var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await httpClient.SendAsync(request);

            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ResponseDto>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}