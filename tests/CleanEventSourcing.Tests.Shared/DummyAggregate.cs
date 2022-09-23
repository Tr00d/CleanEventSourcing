using CleanEventSourcing.Domain;
using LanguageExt;

namespace CleanEventSourcing.Tests.Shared
{
    public class DummyAggregate : IAggregate
    {
        public int EventCount { get; set; }
        public Guid Id { get; set; }

        public Option<IEnumerable<IIntegrationEvent>> GetIntegrationEvents() =>
            new List<DummyEvent> { new() };

        public Option<string> GetStream() => $"{nameof(DummyAggregate)}-{this.Id}";
    }
}