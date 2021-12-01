using System;
using Dawn;
using System.Threading.Tasks;
using CleanEventSourcing.Application.Items;
using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Application.Items.GetItem;
using Microsoft.AspNetCore.Mvc;

namespace CleanEventSourcing.Api.Items
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IService service;
        public ItemsController(IService service)
        {
            this.service = Guard.Argument(service, nameof(service)).NotNull().Value;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateItemRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] GetItemRequest request)
        {
            throw new NotImplementedException();
        }
    }
}