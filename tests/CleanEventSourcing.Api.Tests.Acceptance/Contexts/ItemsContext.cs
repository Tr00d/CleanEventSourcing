using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CleanEventSourcing.Api.Tests.Acceptance.Contexts
{
    public class ItemsContext
    {
        public HttpResponseMessage CreateItemResponse { get; set; }
        
        public HttpResponseMessage GetItemResponse { get; set; }

        public async Task<Guid> GetCreatedIdAsync() =>
            await this.DeserializeResponse<Guid>(this.CreateItemResponse).ConfigureAwait(false);

        private async Task<T> DeserializeResponse<T>(HttpResponseMessage response) =>
            JsonConvert.DeserializeObject<T>(await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync()
                .ConfigureAwait(false));
    }
}