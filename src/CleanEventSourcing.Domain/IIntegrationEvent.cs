using System;
using LanguageExt;
using MediatR;

namespace CleanEventSourcing.Domain
{
    public interface IIntegrationEvent : INotification
    {
        Option<string> Stream { get; }

        DateTime CreationDate { get; }

        Type GetEventType();

        bool CanConvertTo<T>() where T : IAggregate;

        Option<IIntegrationEvent<T>> ConvertTo<T>() where T : IAggregate;
    }

    public interface IIntegrationEvent<T> : IIntegrationEvent
        where T : IAggregate
    {
        void Apply(Option<T> aggregate);
    }
}