using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Domain;
using LanguageExt;
using MediatR;
using static LanguageExt.Prelude;

namespace CleanEventSourcing.Persistence.EventStore
{
    public class InMemoryEventStore : IEventStore, INotificationHandler<IIntegrationEvent>
    {
        private static readonly List<IIntegrationEvent> Events = new();

        public Task<Option<IEnumerable<IIntegrationEvent>>> GetEvents(Option<string> stream) =>
            Task.FromResult(Some(stream.Match(
                streamValue => Events.Where(data => data.Stream.IfNone(string.Empty).Equals(streamValue)),
                new List<IIntegrationEvent>())));

        public Task Handle(IIntegrationEvent notification, CancellationToken cancellationToken)
        {
            Events.Add(notification);
            return Task.CompletedTask;
        }
    }
}