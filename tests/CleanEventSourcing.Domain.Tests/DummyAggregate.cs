using System;
using System.Collections.Generic;
using LanguageExt;

namespace CleanEventSourcing.Domain.Tests
{
    public class DummyAggregate : IAggregate
    {
        public int EventCount { get; set; }
        public Guid Id { get; set; }

        public Option<IEnumerable<IIntegrationEvent>> GetIntegrationEvents() =>
            new List<DummyEvent> { new DummyEvent() };

        public Option<string> GetStream() => $"{nameof(DummyAggregate)}-{this.Id}";
    }
}