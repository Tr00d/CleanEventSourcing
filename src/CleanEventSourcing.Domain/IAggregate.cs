using System;
using System.Collections.Generic;

namespace CleanEventSourcing.Domain
{
    public interface IAggregate
    {
        Guid Id { get; }

        string GetStream();

        IEnumerable<IIntegrationEvent> GetIntegrationEvents();
    }
}