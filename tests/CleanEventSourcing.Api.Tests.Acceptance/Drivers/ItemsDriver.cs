using System;
using System.Net.Http;
using System.Threading.Tasks;
using CleanEventSourcing.Api.Tests.Acceptance.Contexts;
using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Application.Items.GetItem;
using CleanEventSourcing.Application.Items.UpdateItem;

namespace CleanEventSourcing.Api.Tests.Acceptance.Drivers
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ItemsDriver
    {
        private readonly HttpClientDriver clientDriver;
        private readonly ItemsContext context;

        public ItemsDriver(ItemsContext context, HttpClientDriver clientDriver)
        {
            this.context = context;
            this.clientDriver = clientDriver;
        }

        public async Task CreateItem(string description) =>
            this.context.CreateItemResponse =
                await this.clientDriver
                    .ProcessRequest(HttpMethod.Post, "/api/items", new CreateItemRequest {Description = description})
                    .ConfigureAwait(false);

        public async Task GetItem(Guid id) =>
            this.context.GetItemResponse =
                await this.clientDriver
                    .ProcessRequest(HttpMethod.Get, $"/api/items/{id}")
                    .ConfigureAwait(false);

        public async Task UpdateItem(Guid id, string description)
            => this.context.UpdateItemResponse =
                await this.clientDriver
                    .ProcessRequest(HttpMethod.Put, $"/api/items/{id}",
                        new UpdateItemBodyRequest {Description = description})
                    .ConfigureAwait(false);

        public async Task GetItemUsingLocationHeader()
        {
            this.context.GetItemResponse =
                await this.clientDriver
                    .ProcessRequest(HttpMethod.Get, this.context.CreateItemResponse.Headers.Location.AbsolutePath)
                    .ConfigureAwait(false);
        }

        public async Task<GetItemResponse> GetRetrievedItemAsync() => await this.context.GetRetrievedItemAsync();
    }
}