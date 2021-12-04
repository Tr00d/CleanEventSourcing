using System.Threading.Tasks;
using CleanEventSourcing.Application.Items;
using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Application.Items.GetItem;
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
            await this.itemService.CreateAsync(request).ConfigureAwait(false);
            return this.CreatedAtAction("Get", new GetItemRequest {Id = request.Id}, request.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] GetItemRequest request) =>
            (await this.itemService.GetAsync(request).ConfigureAwait(false)).Match(this.Ok,
                (IActionResult) this.NotFound(request));
    }
}