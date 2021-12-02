using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using CleanEventSourcing.Application.Items;
using CleanEventSourcing.Application.Items.CreateItem;
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
            fixture = new Fixture();
            mockMediator = new Mock<IMediator>();
            mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_GivenMediatorIsNull()
        {
            Action instantiation = () => new ItemService(null, mockMapper.Object);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("mediator");
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_GivenMapperIsNull()
        {
            Action instantiation = () => new ItemService(mockMediator.Object, null);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("mapper");
        }

        [Fact]
        public async Task CreateAsync_ShouldSendCommand_GivenRequestContainsValue()
        {
            CreateItemRequest request = fixture.Create<CreateItemRequest>();
            CreateItemCommand command = fixture.Create<CreateItemCommand>();
            mockMapper.Setup(mapper => mapper.Map<CreateItemCommand>(request)).Returns(command);
            ItemService service = new ItemService(mockMediator.Object, mockMapper.Object);
            await service.CreateAsync(request).ConfigureAwait(false);
            mockMediator.Verify(mediator => mediator.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldNotSendCommand_GivenRequestContainsNone()
        {
            ItemService service = new ItemService(mockMediator.Object, mockMapper.Object);
            await service.CreateAsync(Option<CreateItemRequest>.None).ConfigureAwait(false);
            mockMediator.Verify(mediator => mediator.Send(It.IsAny<IRequest>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}