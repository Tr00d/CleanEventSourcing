using System.Net.Http;
using System.Threading.Tasks;
using CleanEventSourcing.Api.Tests.Acceptance.Contexts;
using CleanEventSourcing.Application.Items.CreateItem;

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
            this.context.CreationResponse =
            await this.clientDriver
                .ProcessRequest(HttpMethod.Post, "/api/items", new CreateItemRequest {Description = description})
                .ConfigureAwait(false);
    }
}