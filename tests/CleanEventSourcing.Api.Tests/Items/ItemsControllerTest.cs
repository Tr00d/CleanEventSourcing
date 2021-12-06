using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using CleanEventSourcing.Api.Items;
using CleanEventSourcing.Application.Items;
using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Application.Items.GetItem;
using CleanEventSourcing.Application.Items.UpdateItem;
using FluentAssertions;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

// ReSharper disable All

namespace CleanEventSourcing.Api.Tests.Items
{
    public class ItemsControllerTest
    {
        private readonly Fixture fixture;
        private readonly Mock<IItemService> mockService;

        public ItemsControllerTest()
        {
            this.mockService = new Mock<IItemService>();
            this.fixture = new Fixture();
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_GivenItemServiceIsNull()
        {
            Action instantiation = () => new ItemsController(null);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("itemService");
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedStatus()
        {
            CreateItemRequest request = this.fixture.Create<CreateItemRequest>();
            ItemsController controller = new ItemsController(this.mockService.Object);
            IActionResult result = await controller.CreateAsync(request).ConfigureAwait(false);
            result.Should().BeOfType<CreatedAtActionResult>();
            CreatedAtActionResult createdResult = (CreatedAtActionResult) result;
            createdResult.ActionName.Should().Be("Get");
            createdResult.Value.Should().Be(request.Id);
            createdResult.RouteValues.First().Key.Should().Be(nameof(GetItemRequest.Id));
            createdResult.RouteValues.First().Value.Should().Be(request.Id);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateItem()
        {
            CreateItemRequest request = this.fixture.Create<CreateItemRequest>();
            ItemsController controller = new ItemsController(this.mockService.Object);
            await controller.CreateAsync(request).ConfigureAwait(false);
            this.mockService.Verify(service => service.CreateAsync(request), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNotFound_GivenRetrievedItemIsNone()
        {
            GetItemRequest request = this.fixture.Create<GetItemRequest>();
            this.mockService.Setup(service => service.GetAsync(request))
                .ReturnsAsync(Option<GetItemResponse>.None);
            ItemsController controller = new ItemsController(this.mockService.Object);
            IActionResult result = await controller.GetAsync(request).ConfigureAwait(false);
            result.Should().BeOfType<NotFoundObjectResult>();
            NotFoundObjectResult notFoundResult = (NotFoundObjectResult) result;
            notFoundResult.Value.Should().Be(request);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnOk_GivenRetrievedItemIsSome()
        {
            GetItemResponse response = this.fixture.Create<GetItemResponse>();
            GetItemRequest request = this.fixture.Create<GetItemRequest>();
            this.mockService.Setup(service => service.GetAsync(request))
                .ReturnsAsync(response);
            ItemsController controller = new ItemsController(this.mockService.Object);
            IActionResult result = await controller.GetAsync(request).ConfigureAwait(false);
            result.Should().BeOfType<OkObjectResult>();
            OkObjectResult okResult = (OkObjectResult) result;
            okResult.Value.Should().Be(response);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateItem()
        {
            UpdateItemBodyRequest bodyRequest = this.fixture.Create<UpdateItemBodyRequest>();
            UpdateItemRouteRequest routeRequest = this.fixture.Create<UpdateItemRouteRequest>();
            ItemsController controller = new ItemsController(this.mockService.Object);
            await controller.UpdateAsync(routeRequest, bodyRequest).ConfigureAwait(false);
            this.mockService.Verify(service => service.UpdateAsync(routeRequest, bodyRequest), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnNoContent()
        {
            UpdateItemBodyRequest bodyRequest = this.fixture.Create<UpdateItemBodyRequest>();
            UpdateItemRouteRequest routeRequest = this.fixture.Create<UpdateItemRouteRequest>();
            ItemsController controller = new ItemsController(this.mockService.Object);
            IActionResult result = await controller.UpdateAsync(routeRequest, bodyRequest).ConfigureAwait(false);
            result.Should().BeOfType<NoContentResult>();
        }
    }
}