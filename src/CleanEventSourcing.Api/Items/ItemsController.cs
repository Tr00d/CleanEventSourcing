using System.Threading.Tasks;
using CleanEventSourcing.Application.Items;
using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Application.Items.DeleteItem;
using CleanEventSourcing.Application.Items.GetItem;
using CleanEventSourcing.Application.Items.UpdateItem;
using Dawn;
using Microsoft.AspNetCore.Mvc;

namespace CleanEventSourcing.Api.Items
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService itemService;

        public ItemsController(IItemService itemService)
        {
            this.itemService = Guard.Argument(itemService, nameof(itemService)).NotNull().Value;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateItemRequest request)
        {
            await this.itemService.CreateAsync(request);
            return this.CreatedAtAction("Get", new GetItemRequest { Id = request.Id }, request.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] GetItemRequest request) =>
            (await this.itemService.GetAsync(request)).Match(this.Ok,
                (IActionResult)this.NotFound(request));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] UpdateItemRouteRequest routeRequest,
            [FromBody] UpdateItemBodyRequest bodyRequest)
        {
            await this.itemService.UpdateAsync(routeRequest, bodyRequest);
            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] DeleteItemRequest request)
        {
            await this.itemService.DeleteAsync(request);
            return this.NoContent();
        }
    }
}