using System;
using System.Net.Http;
using System.Threading.Tasks;
using CleanEventSourcing.Application.Items.GetItem;
using Newtonsoft.Json;

namespace CleanEventSourcing.Api.Tests.Acceptance.Contexts
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ItemsContext
    {
        public HttpResponseMessage CreateItemResponse { get; set; }

        public HttpResponseMessage GetItemResponse { get; set; }

        public HttpResponseMessage UpdateItemResponse { get; set; }

        public async Task<Guid> GetCreatedIdAsync() =>
            await DeserializeResponse<Guid>(this.CreateItemResponse).ConfigureAwait(false);

        public async Task<GetItemResponse> GetRetrievedItemAsync() =>
            await DeserializeResponse<GetItemResponse>(this.GetItemResponse).ConfigureAwait(false);

        private static async Task<T> DeserializeResponse<T>(HttpResponseMessage response) =>
            JsonConvert.DeserializeObject<T>(await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync()
                .ConfigureAwait(false));
    }
}