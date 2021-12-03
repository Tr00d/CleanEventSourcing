using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Domain;
using CleanEventSourcing.Domain.Items.Events;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace CleanEventSourcing.Application.Tests.Items.CreateItem
{
    public class CreateItemHandlerTest
    {
        private readonly Fixture fixture;
        private readonly Mock<IEventStore> mockEventStore;
        private readonly Mock<IMediator> mockMediator;

        public CreateItemHandlerTest()
        {
            this.fixture = new Fixture();
            this.mockEventStore = new Mock<IEventStore>();
            this.mockMediator = new Mock<IMediator>();
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_GivenEventStoreIsNull()
        {
            Action instantiation = () => new CreateItemHandler(null, this.mockMediator.Object);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("eventStore");
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_GivenMediatorIsNull()
        {
            Action instantiation = () => new CreateItemHandler(this.mockEventStore.Object, null);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("mediator");
        }

        [Fact]
        public async Task Handle_ShouldPublishCreatedItemEvent()
        {
            CreateItemCommand command = this.fixture.Create<CreateItemCommand>();
            string stream = $"Item-{command.Id}";
            CreatedItemEvent expectedEvent = new CreatedItemEvent(stream, command.Id, command.Description);
            CreateItemHandler handler = new CreateItemHandler(this.mockEventStore.Object, this.mockMediator.Object);
            await handler.Handle(command, this.fixture.Create<CancellationToken>());
            this.mockMediator.Verify(mediator
                    => mediator.Publish<IIntegrationEvent>(
                        It.Is<CreatedItemEvent>(input =>
                            input.Id.Equals(expectedEvent.Id) && input.Description.Equals(expectedEvent.Description) &&
                            input.Stream.Equals(expectedEvent.Stream)),
                        It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}