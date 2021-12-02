using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using CleanEventSourcing.Domain;
using CleanEventSourcing.Persistence.EventStore;
using FluentAssertions;
using LanguageExt;
using Xunit;

namespace CleanEventSourcing.Persistence.Tests.EventStore
{
    public class InMemoryEventStoreTest
    {
        private readonly Fixture fixture;

        public InMemoryEventStoreTest()
        {
            fixture = new Fixture();
        }

        [Fact]
        public async Task PublishEvents_ShouldStoreEvents()
        {
            string stream = fixture.Create<string>();
            DummyEvent[] events = fixture.CreateMany<DummyEvent>().ToArray();
            InMemoryEventStore eventStore = new InMemoryEventStore();
            await eventStore.PublishEventsAsync(stream, events);
            IEnumerable<IIntegrationEvent> savedEvents = await eventStore.GetEvents(stream);
            IIntegrationEvent[] integrationEvents = savedEvents.ToArray();
            integrationEvents.Should().HaveCount(events.Length);
            integrationEvents.Should().BeEquivalentTo(events);
        }

        private class DummyEvent : IIntegrationEvent
        {
            public DateTime CreationDate { get; }

            public Type GetEventType()
            {
                throw new NotImplementedException();
            }

            public bool CanConvertTo<T>() where T : IAggregate
            {
                throw new NotImplementedException();
            }

            public Option<IIntegrationEvent<T>> TryConvertTo<T>() where T : IAggregate
            {
                throw new NotImplementedException();
            }
        }
    }
}