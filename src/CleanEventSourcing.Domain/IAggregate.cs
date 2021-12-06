using System;
using System.Collections.Generic;
using System.ComponentModel;
using LanguageExt;

namespace CleanEventSourcing.Domain
{
    public interface IAggregate
    {
        Guid Id { get; }

        Option<string> GetStream();

        Option<IEnumerable<IIntegrationEvent>> GetIntegrationEvents();
    }
}