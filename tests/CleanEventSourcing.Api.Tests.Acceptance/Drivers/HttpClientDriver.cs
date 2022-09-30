using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CleanEventSourcing.Api.Tests.Acceptance.Contexts;
using Newtonsoft.Json;

namespace CleanEventSourcing.Api.Tests.Acceptance.Drivers
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class HttpClientDriver
    {
        private readonly HttpClient client;

        public HttpClientDriver(AcceptanceContext context)
        {
            this.client = context.HttpClient;
        }

        public async Task<HttpResponseMessage> ProcessRequest(HttpMethod method, string relativeUri) =>
            await this.ProcessRequest(this.CreateRequest(method, relativeUri));

        public async Task<HttpResponseMessage> ProcessRequest<TRequest>(HttpMethod method, string relativeUri,
            TRequest data) => await this.ProcessRequest(this.CreateRequest(method, relativeUri, data));

        private HttpRequestMessage CreateRequest<T>(HttpMethod method, string relativeUri, T data)
        {
            var requestMessage = this.CreateRequest(method, relativeUri);
            requestMessage.Content =
                new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            return requestMessage;
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string relativeUri) =>
            new()
            {
                Method = method,
                RequestUri = new Uri(this.client.BaseAddress, relativeUri),
            };

        private async Task<HttpResponseMessage> ProcessRequest(HttpRequestMessage message) =>
            await this.client.SendAsync(message);
    }
}