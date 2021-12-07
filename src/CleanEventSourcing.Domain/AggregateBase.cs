using System;
using System.Collections.Generic;

namespace CleanEventSourcing.Domain
{
    public class AggregateBase
    {
        private readonly List<IIntegrationEvent> events = new();

        public Guid Id { get; set; }

        public IEnumerable<IIntegrationEvent> GetEvents() => new List<IIntegrationEvent>(this.events);

        public void AddEvent(IIntegrationEvent integrationEvent) => this.events.Add(integrationEvent);
    }
}