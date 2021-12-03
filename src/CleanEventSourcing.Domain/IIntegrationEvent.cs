using System;
using LanguageExt;

namespace CleanEventSourcing.Domain
{
    public interface IIntegrationEvent
    {
        DateTime CreationDate { get; }

        Type GetEventType();

        bool CanConvertTo<T>() where T : IAggregate;

        Option<IIntegrationEvent<T>> TryConvertTo<T>() where T : IAggregate;
    }

    public interface IIntegrationEvent<T> : IIntegrationEvent
        where T : IAggregate
    {
        void Apply(Option<T> aggregate);
    }
}