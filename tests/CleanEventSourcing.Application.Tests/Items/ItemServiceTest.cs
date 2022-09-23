using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using CleanEventSourcing.Application.Items;
using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Application.Items.DeleteItem;
using CleanEventSourcing.Application.Items.GetItem;
using CleanEventSourcing.Application.Items.UpdateItem;
using CleanEventSourcing.Domain.Items;
using FluentAssertions;
using LanguageExt;
using MediatR;
using Moq;
using Xunit;
using static LanguageExt.Prelude;

namespace CleanEventSourcing.Application.Tests.Items
{
    public class ItemServiceTest
    {
        private readonly Fixture fixture;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IMediator> mockMediator;

        public ItemServiceTest()
        {
            this.fixture = new Fixture();
            this.mockMediator = new Mock<IMediator>();
            this.mockMapper = new Mock<IMapper>();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Constructor_ShouldThrowArgumentNullException_GivenMediatorIsNull()
        {
            Action instantiation = () => new ItemService(null, this.mockMapper.Object);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("mediator");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Constructor_ShouldThrowArgumentNullException_GivenMapperIsNull()
        {
            Action instantiation = () => new ItemService(this.mockMediator.Object, null);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("mapper");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task CreateAsync_ShouldSendCommand_GivenRequestContainsSome()
        {
            var request = this.fixture.Create<CreateItemRequest>();
            var command = this.fixture.Create<CreateItemCommand>();
            this.mockMapper.Setup(mapper => mapper.Map<CreateItemCommand>(request)).Returns(command);
            var service = new ItemService(this.mockMediator.Object, this.mockMapper.Object);
            await service.CreateAsync(request).ConfigureAwait(false);
            this.mockMediator.Verify(mediator => mediator.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task CreateAsync_ShouldNotSendCommand_GivenRequestContainsNone()
        {
            var service = new ItemService(this.mockMediator.Object, this.mockMapper.Object);
            await service.CreateAsync(Option<CreateItemRequest>.None).ConfigureAwait(false);
            this.mockMediator.Verify(mediator => mediator.Send(It.IsAny<IRequest>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetAsync_ShouldNotSendQuery_GivenRequestContainsNone()
        {
            var service = new ItemService(this.mockMediator.Object, this.mockMapper.Object);
            await service.GetAsync(Option<GetItemRequest>.None).ConfigureAwait(false);
            this.mockMediator.Verify(mediator => mediator.Send(It.IsAny<IRequest>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetAsync_ShouldReturnNone_GivenRequestContainsNone()
        {
            var service = new ItemService(this.mockMediator.Object, this.mockMapper.Object);
            var response =
                await service.GetAsync(Option<GetItemRequest>.None).ConfigureAwait(false);
            response.IsNone.Should().Be(true);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetAsync_ShouldReturnSome_GivenRequestContainsSome()
        {
            var request = this.fixture.Create<GetItemRequest>();
            var query = this.fixture.Create<GetItemQuery>();
            var response = this.fixture.Create<GetItemResponse>();
            var summary = this.fixture.Create<ItemSummary>();
            this.mockMapper.Setup(mapper => mapper.Map<GetItemQuery>(request)).Returns(query);
            this.mockMapper.Setup(mapper => mapper.Map<GetItemResponse>(summary)).Returns(response);
            this.mockMediator.Setup(mediator => mediator.Send(query, It.IsAny<CancellationToken>()))
                .ReturnsAsync(summary);
            var service = new ItemService(this.mockMediator.Object, this.mockMapper.Object);
            var result = await service.GetAsync(request).ConfigureAwait(false);
            result.IsSome.Should().Be(true);
            result.IfNone(new GetItemResponse()).Should().Be(response);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task UpdateAsync_ShouldSendUpdateItemCommand_GivenRequestIsSome()
        {
            var bodyRequest = this.fixture.Create<UpdateItemBodyRequest>();
            var routeRequest = this.fixture.Create<UpdateItemRouteRequest>();
            var command = this.fixture.Create<UpdateItemCommand>();
            this.mockMapper.Setup(mapper => mapper.Map<UpdateItemCommand>(Tuple(routeRequest, bodyRequest)))
                .Returns(command);
            var service = new ItemService(this.mockMediator.Object, this.mockMapper.Object);
            await service.UpdateAsync(routeRequest, bodyRequest);
            this.mockMediator.Verify(mediator => mediator.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task UpdateAsync_ShouldNotSendUpdateItemCommand_GivenRouteRequestIsNone()
        {
            var bodyRequest = this.fixture.Create<UpdateItemBodyRequest>();
            var service = new ItemService(this.mockMediator.Object, this.mockMapper.Object);
            await service.UpdateAsync(Option<UpdateItemRouteRequest>.None, bodyRequest);
            this.mockMediator.Verify(
                mediator => mediator.Send(It.IsAny<UpdateItemCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task UpdateAsync_ShouldNotSendUpdateItemCommand_GivenBodyRequestIsNone()
        {
            var routeRequest = this.fixture.Create<UpdateItemRouteRequest>();
            var service = new ItemService(this.mockMediator.Object, this.mockMapper.Object);
            await service.UpdateAsync(routeRequest, Option<UpdateItemBodyRequest>.None);
            this.mockMediator.Verify(
                mediator => mediator.Send(It.IsAny<UpdateItemCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task DeleteAsync_ShouldNotSendDeleteItemCommand_GivenRequestIsNone()
        {
            var service = new ItemService(this.mockMediator.Object, this.mockMapper.Object);
            await service.DeleteAsync(Option<DeleteItemRequest>.None);
            this.mockMediator.Verify(
                mediator => mediator.Send(It.IsAny<DeleteItemCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task DeleteAsync_ShouldSendDeleteItemCommand_GivenRequestIsSome()
        {
            var request = this.fixture.Create<DeleteItemRequest>();
            var command = this.fixture.Create<DeleteItemCommand>();
            this.mockMapper.Setup(mapper => mapper.Map<DeleteItemCommand>(request)).Returns(command);
            var service = new ItemService(this.mockMediator.Object, this.mockMapper.Object);
            await service.DeleteAsync(request);
            this.mockMediator.Verify(
                mediator => mediator.Send(It.IsAny<DeleteItemCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}