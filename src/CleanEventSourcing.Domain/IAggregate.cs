using System;
using System.Collections.Generic;
using System.ComponentModel;
using LanguageExt;

namespace CleanEventSourcing.Domain
{
    public interface IAggregate
    {
        Guid Id { get; }

        Option<IEnumerable<IIntegrationEvent>> GetIntegrationEvents();
    }
}