using System;
using System.Net.Http;
using System.Threading.Tasks;
using CleanEventSourcing.Application.Items.GetItem;
using Newtonsoft.Json;

namespace CleanEventSourcing.Api.Tests.Acceptance.Contexts
{
    public class ItemsContext
    {
        public HttpResponseMessage CreateItemResponse { get; set; }

        public HttpResponseMessage GetItemResponse { get; set; }

        public HttpResponseMessage UpdateItemResponse { get; set; }

        public HttpResponseMessage DeleteItemResponse { get; set; }

        public async Task<Guid> GetCreatedIdAsync() =>
            await DeserializeResponse<Guid>(this.CreateItemResponse);

        public async Task<GetItemResponse> GetRetrievedItemAsync() =>
            await DeserializeResponse<GetItemResponse>(this.GetItemResponse);

        private static async Task<T> DeserializeResponse<T>(HttpResponseMessage response) =>
            JsonConvert.DeserializeObject<T>(await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync()
            );
    }
}