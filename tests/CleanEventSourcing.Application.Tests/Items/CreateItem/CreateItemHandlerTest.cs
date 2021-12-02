using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Domain;
using CleanEventSourcing.Domain.Items.Events;
using FluentAssertions;
using Moq;
using Xunit;

namespace CleanEventSourcing.Application.Tests.Items.CreateItem
{
    public class CreateItemHandlerTest
    {
        private readonly Fixture fixture;
        private readonly Mock<IEventStore> mockEventStore;

        public CreateItemHandlerTest()
        {
            fixture = new Fixture();
            mockEventStore = new Mock<IEventStore>();
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_GivenEventStoreIsNull()
        {
            Action instantiation = () => new CreateItemHandler(null);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("eventStore");
        }

        [Fact]
        public async Task Handle_ShouldPublishEvents()
        {
            IIntegrationEvent expectedEvent = fixture.Create<CreatedItemEvent>();
            CreateItemCommand command = fixture.Create<CreateItemCommand>();
            CreateItemHandler handler = new CreateItemHandler(mockEventStore.Object);
            await handler.Handle(command, fixture.Create<CancellationToken>());
            mockEventStore.Verify(
                eventStore =>
                    eventStore.PublishEventsAsync($"Item-{command.Id}", It.IsAny<IEnumerable<IIntegrationEvent>>()),
                Times.Once);
            IEnumerable<IIntegrationEvent> argument = (IEnumerable<IIntegrationEvent>) mockEventStore.Invocations
                .First().Arguments.First(argument => argument is IEnumerable<IIntegrationEvent>);
            argument.First().Should().BeEquivalentTo(expectedEvent);
        }
    }
}