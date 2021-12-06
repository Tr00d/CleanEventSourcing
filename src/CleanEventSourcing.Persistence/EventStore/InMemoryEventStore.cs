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
        private static List<IIntegrationEvent> events;

        public InMemoryEventStore()
        {
            if (events is null)
            {
                events = new List<IIntegrationEvent>();
            }
        }

        public Task<Option<IEnumerable<IIntegrationEvent>>> GetEvents(Option<string> stream) =>
            Task.FromResult(Some(stream.Match(
                streamValue => events.Where(data => data.Stream.IfNone(string.Empty).Equals(streamValue)),
                new List<IIntegrationEvent>())));

        public Task Handle(IIntegrationEvent notification, CancellationToken cancellationToken)
        {
            events.Add(notification);
            return Task.CompletedTask;
        }
    }
}