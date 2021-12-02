using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Domain;

namespace CleanEventSourcing.Persistence.EventStore
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly Dictionary<string, List<IIntegrationEvent>> eventData;

        public InMemoryEventStore()
        {
            eventData = new Dictionary<string, List<IIntegrationEvent>>();
        }

        public Task PublishEventsAsync(string stream, IEnumerable<IIntegrationEvent> events)
        {
            if (!eventData.ContainsKey(stream))
            {
                eventData.Add(stream, new List<IIntegrationEvent>());
            }

            eventData[stream].AddRange(events);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<IIntegrationEvent>> GetEvents(string stream) =>
            Task.FromResult(eventData.Where(pair => pair.Key == stream).SelectMany(pair => pair.Value));
    }
}