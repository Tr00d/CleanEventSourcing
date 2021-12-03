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
using LanguageExt;
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
            this.fixture = new Fixture();
            this.mockEventStore = new Mock<IEventStore>();
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
            IIntegrationEvent expectedEvent = this.fixture.Create<CreatedItemEvent>();
            CreateItemCommand command = this.fixture.Create<CreateItemCommand>();
            CreateItemHandler handler = new CreateItemHandler(this.mockEventStore.Object);
            await handler.Handle(command, this.fixture.Create<CancellationToken>());
            this.mockEventStore.Verify(
                eventStore =>
                    eventStore.PublishEventsAsync($"Item-{command.Id}",
                        It.IsAny<Option<IEnumerable<IIntegrationEvent>>>()),
                Times.Once);
            Option<IEnumerable<IIntegrationEvent>> argument = (Option<IEnumerable<IIntegrationEvent>>) this.mockEventStore
                .Invocations
                .First().Arguments.First(argument => argument is Option<IEnumerable<IIntegrationEvent>>);
            argument.IfNone(new List<IIntegrationEvent>()).First().Should().BeEquivalentTo(expectedEvent);
        }
    }
}