using System;
using LanguageExt;
using static LanguageExt.Prelude;

namespace CleanEventSourcing.Domain.Tests
{
    public class DummyEvent : IIntegrationEvent, IIntegrationEvent<DummyAggregate>
    {
        public Guid Id { get; }
        public Option<string> Stream { get; set; }
        public bool CanConvertTo<T>() where T : IAggregate => typeof(T) == typeof(DummyAggregate);

        public Option<IIntegrationEvent<T>> ConvertTo<T>() where T : IAggregate => this.CanConvertTo<T>()
            ? Some((IIntegrationEvent<T>) this)
            : Option<IIntegrationEvent<T>>.None;

        public void Apply(Option<DummyAggregate> aggregate) => aggregate.IfSome(value => value.EventCount++);
    }
}