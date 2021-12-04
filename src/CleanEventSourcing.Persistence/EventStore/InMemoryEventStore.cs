using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Domain;
using LanguageExt;
using MediatR;
using static LanguageExt.Prelude;

namespace CleanEventSourcing.Persistence.EventStore
{
    public class InMemoryEventStore : IEventStore, INotificationHandler<IIntegrationEvent>
    {
        private readonly List<IIntegrationEvent> events;

        public InMemoryEventStore()
        {
            this.events = new List<IIntegrationEvent>();
        }

        public Task<Option<IEnumerable<IIntegrationEvent>>> GetEvents(Option<string> stream) =>
            Task.FromResult(Some(stream.Match(
                streamValue => this.events.Where(data => data.Stream.IfNone(string.Empty).Equals(streamValue)),
                new List<IIntegrationEvent>())));

        public Task Handle(IIntegrationEvent notification, CancellationToken cancellationToken)
        {
            this.events.Add(notification);
            return Task.CompletedTask;
        }
    }
}