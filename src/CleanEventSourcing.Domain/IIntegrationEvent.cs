using System;
using LanguageExt;
using MediatR;

namespace CleanEventSourcing.Domain
{
    public interface IIntegrationEvent : INotification
    {
        public Guid Id { get; }
        
        public Option<string> Stream { get; set; }
        
        bool CanConvertTo<T>() where T : IAggregate;

        Option<IIntegrationEvent<T>> ConvertTo<T>() where T : IAggregate;
    }

    public interface IIntegrationEvent<T> : IIntegrationEvent
        where T : IAggregate
    {
        void Apply(Option<T> aggregate);
    }
}