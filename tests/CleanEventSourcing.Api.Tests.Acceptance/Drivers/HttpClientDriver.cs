using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CleanEventSourcing.Api.Tests.Acceptance.Contexts;
using Newtonsoft.Json;

namespace CleanEventSourcing.Api.Tests.Acceptance.Drivers
{
    public class HttpClientDriver
    {
        private readonly HttpClient client;
        private readonly HttpScenarioContext httpScenarioContext;

        public HttpClientDriver(HttpScenarioContext context)
        {
            httpScenarioContext = context;
            client = context.Client;
        }

        public async Task<HttpResponseMessage> ProcessRequest(HttpMethod method, string relativeUri) =>
            await ProcessRequest(CreateRequest(method, relativeUri)).ConfigureAwait(false);

        public async Task<HttpResponseMessage> ProcessRequest<TRequest>(HttpMethod method, string relativeUri,
            TRequest data) => await ProcessRequest(CreateRequest(method, relativeUri, data)).ConfigureAwait(false);

        public async Task<TResponse> ProcessRequest<TResponse, TRequest>(HttpMethod method, string relativeUri,
            TRequest data)
        {
            HttpResponseMessage response = await ProcessRequest(method, relativeUri, data).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TResponse>(await response.EnsureSuccessStatusCode().Content
                .ReadAsStringAsync().ConfigureAwait(false));
        }

        public async Task<TResponse> ProcessRequest<TResponse>(HttpMethod method, string relativeUri)
        {
            HttpResponseMessage response = await ProcessRequest(method, relativeUri).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TResponse>(await response.EnsureSuccessStatusCode().Content
                .ReadAsStringAsync().ConfigureAwait(false));
        }

        private HttpRequestMessage CreateRequest<T>(HttpMethod method, string relativeUri, T data)
        {
            HttpRequestMessage requestMessage = CreateRequest(method, relativeUri);
            requestMessage.Content =
                new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            return requestMessage;
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string relativeUri) =>
            new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(client.BaseAddress, relativeUri)
            };

        private async Task<HttpResponseMessage> ProcessRequest(HttpRequestMessage message) =>
            await client.SendAsync(message).ConfigureAwait(false);
    }
}