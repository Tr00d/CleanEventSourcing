using System;
using System.Net.Http;
using System.Threading.Tasks;
using CleanEventSourcing.Api.Tests.Acceptance.Contexts;
using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Application.Items.GetItem;

namespace CleanEventSourcing.Api.Tests.Acceptance.Drivers
{
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

        public async Task GetItemUsingLocationHeader()
        {
            this.context.GetItemResponse =
                await this.clientDriver
                    .ProcessRequest(HttpMethod.Get, this.context.CreateItemResponse.Headers.Location.AbsolutePath)
                    .ConfigureAwait(false);
        }

        public async Task<GetItemResponse> GetCreatedItemAsync() => await this.context.GetCreatedItemAsync();
    }
}