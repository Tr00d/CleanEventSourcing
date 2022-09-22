using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CleanEventSourcing.Domain;
using CleanEventSourcing.Persistence.EventStore;
using FluentAssertions;
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
            var stream = this.fixture.Create<string>();
            var mockEvent = new Mock<IIntegrationEvent>();
            mockEvent.SetupGet(mock => mock.Stream).Returns(stream);
            var eventStore = new InMemoryEventStore();
            await eventStore.Handle(mockEvent.Object, CancellationToken.None);
            var savedEvents = await eventStore.GetEvents(stream);
            savedEvents.IsSome.Should().Be(true);
            var integrationEvents = savedEvents.IfNone(Enumerable.Empty<IIntegrationEvent>()).ToArray();
            integrationEvents.Should().HaveCount(1);
            integrationEvents.First().Should().BeEquivalentTo(mockEvent.Object);
        }
    }
}