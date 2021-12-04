using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CleanEventSourcing.Domain;
using CleanEventSourcing.Persistence.EventStore;
using FluentAssertions;
using LanguageExt;
using Moq;
using Xunit;

namespace CleanEventSourcing.Persistence.Tests.EventStore
{
    public class InMemoryEventStoreTest
    {
        private readonly Fixture fixture;

        public InMemoryEventStoreTest()
        {
            this.fixture = new Fixture();
        }

        [Fact]
        public async Task HandleCreatedItemEvent_ShouldStoreEvent()
        {
            string stream = this.fixture.Create<string>();
            Mock<IIntegrationEvent> mockEvent = new Mock<IIntegrationEvent>();
            mockEvent.SetupGet(mock => mock.Stream).Returns(stream);
            InMemoryEventStore eventStore = new InMemoryEventStore();
            await eventStore.Handle(mockEvent.Object, this.fixture.Create<CancellationToken>());
            Option<IEnumerable<IIntegrationEvent>> savedEvents = await eventStore.GetEvents(stream);
            savedEvents.IsSome.Should().Be(true);
            IIntegrationEvent[] integrationEvents = savedEvents.IfNone(Enumerable.Empty<IIntegrationEvent>()).ToArray();
            integrationEvents.Should().HaveCount(1);
            integrationEvents.First().Should().BeEquivalentTo(mockEvent.Object);
        }
    }
}