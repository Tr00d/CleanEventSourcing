using System;
using System.Threading.Tasks;
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
        public async Task<IActionResult> CreateItemAsync()
        {
            throw new NotImplementedException();
        }
    }
}