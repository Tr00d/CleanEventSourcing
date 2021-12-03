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
            this.eventData = new Dictionary<string, List<IIntegrationEvent>>();
        }

        public Task PublishEventsAsync(Option<string> stream, Option<IEnumerable<IIntegrationEvent>> events)
        {
            stream.IfSome(streamValue => this.PublishEvents(streamValue, events));
            return Task.CompletedTask;
        }

        public Task<Option<IEnumerable<IIntegrationEvent>>> GetEvents(Option<string> stream) =>
            Task.FromResult(stream.Match(
                streamValue => Some(this.GetEvents(streamValue)),
                Option<IEnumerable<IIntegrationEvent>>.None));

        private IEnumerable<IIntegrationEvent> GetEvents(string stream) => this.eventData.Where(pair => pair.Key == stream).SelectMany(pair => pair.Value);

        private void AddEventInDictionary(string stream)
        {
            if (!this.eventData.ContainsKey(stream))
            {
                this.eventData.Add(stream, new List<IIntegrationEvent>());
            }
        }

        private void AppendEventsInDictionary(string stream, IEnumerable<IIntegrationEvent> events) => this.eventData[stream].AddRange(events);

        private void PublishEvents(string stream, Option<IEnumerable<IIntegrationEvent>> events)
        {
            this.AddEventInDictionary(stream);
            this.AppendEventsInDictionary(stream, events.IfNone(new List<IIntegrationEvent>()));
        }
    }
}