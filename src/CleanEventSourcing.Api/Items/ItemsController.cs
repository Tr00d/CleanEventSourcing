using System;
using System.Threading.Tasks;
using CleanEventSourcing.Application.Items.CreateItem;
using Microsoft.AspNetCore.Mvc;

namespace CleanEventSourcing.Api.Items
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        public ItemsController()
        {
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateItemAsync([FromBody] CreateItemRequest request)
        {
            throw new NotImplementedException();
        }
    }
}