using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Domain;
using LanguageExt;
using static LanguageExt.Prelude;

namespace CleanEventSourcing.Persistence.EventStore
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly Dictionary<string, List<IIntegrationEvent>> eventData;

        public InMemoryEventStore()
        {
            eventData = new Dictionary<string, List<IIntegrationEvent>>();
        }

        public Task PublishEventsAsync(Option<string> stream, Option<IEnumerable<IIntegrationEvent>> events)
        {
            stream.IfSome(streamValue => PublishEvents(streamValue, events));
            return Task.CompletedTask;
        }

        public Task<Option<IEnumerable<IIntegrationEvent>>> GetEvents(Option<string> stream) =>
            Task.FromResult(stream.Match(
                streamValue => Some(GetEvents(streamValue)),
                Option<IEnumerable<IIntegrationEvent>>.None));

        private IEnumerable<IIntegrationEvent> GetEvents(string stream) =>
            eventData.Where(pair => pair.Key == stream).SelectMany(pair => pair.Value);

        private void AddEventInDictionary(string stream)
        {
            if (!eventData.ContainsKey(stream))
            {
                eventData.Add(stream, new List<IIntegrationEvent>());
            }
        }

        private void AppendEventsInDictionary(string stream, IEnumerable<IIntegrationEvent> events) =>
            eventData[stream].AddRange(events);

        private void PublishEvents(string stream, Option<IEnumerable<IIntegrationEvent>> events)
        {
            AddEventInDictionary(stream);
            AppendEventsInDictionary(stream, events.IfNone(new List<IIntegrationEvent>()));
        }
    }
}