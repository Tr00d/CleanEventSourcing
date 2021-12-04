using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using CleanEventSourcing.Application.Items;
using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Application.Items.GetItem;
using CleanEventSourcing.Domain.Items;
using FluentAssertions;
using LanguageExt;
using MediatR;
using Moq;
using Xunit;

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
        public void Constructor_ShouldThrowArgumentNullException_GivenMediatorIsNull()
        {
            Action instantiation = () => new ItemService(null, this.mockMapper.Object);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("mediator");
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_GivenMapperIsNull()
        {
            Action instantiation = () => new ItemService(this.mockMediator.Object, null);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("mapper");
        }

        [Fact]
        public async Task CreateAsync_ShouldSendCommand_GivenRequestContainsSome()
        {
            CreateItemRequest request = this.fixture.Create<CreateItemRequest>();
            CreateItemCommand command = this.fixture.Create<CreateItemCommand>();
            this.mockMapper.Setup(mapper => mapper.Map<CreateItemCommand>(request)).Returns(command);
            ItemService service = new ItemService(this.mockMediator.Object, this.mockMapper.Object);
            await service.CreateAsync(request).ConfigureAwait(false);
            this.mockMediator.Verify(mediator => mediator.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldNotSendCommand_GivenRequestContainsNone()
        {
            ItemService service = new ItemService(this.mockMediator.Object, this.mockMapper.Object);
            await service.CreateAsync(Option<CreateItemRequest>.None).ConfigureAwait(false);
            this.mockMediator.Verify(mediator => mediator.Send(It.IsAny<IRequest>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task GetAsync_ShouldNotSendQuery_GivenRequestContainsNone()
        {
            ItemService service = new ItemService(this.mockMediator.Object, this.mockMapper.Object);
            await service.GetAsync(Option<GetItemRequest>.None).ConfigureAwait(false);
            this.mockMediator.Verify(mediator => mediator.Send(It.IsAny<IRequest>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNone_GivenRequestContainsNone()
        {
            ItemService service = new ItemService(this.mockMediator.Object, this.mockMapper.Object);
            Option<GetItemResponse> response =
                await service.GetAsync(Option<GetItemRequest>.None).ConfigureAwait(false);
            response.IsNone.Should().Be(true);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnSome_GivenRequestContainsSome()
        {
            GetItemRequest request = this.fixture.Create<GetItemRequest>();
            GetItemQuery query = this.fixture.Create<GetItemQuery>();
            GetItemResponse response = this.fixture.Create<GetItemResponse>();
            ItemSummary summary = this.fixture.Create<ItemSummary>();
            this.mockMapper.Setup(mapper => mapper.Map<GetItemQuery>(request)).Returns(query);
            this.mockMapper.Setup(mapper => mapper.Map<GetItemResponse>(summary)).Returns(response);
            this.mockMediator.Setup(mediator => mediator.Send(query, It.IsAny<CancellationToken>()))
                .ReturnsAsync(summary);
            ItemService service = new ItemService(this.mockMediator.Object, this.mockMapper.Object);
            Option<GetItemResponse> result = await service.GetAsync(request).ConfigureAwait(false);
            result.IsSome.Should().Be(true);
            result.IfNone(new GetItemResponse()).Should().Be(response);
        }
    }
}