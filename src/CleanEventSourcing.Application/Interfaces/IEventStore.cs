using System.Collections.Generic;
using System.Threading.Tasks;
using CleanEventSourcing.Domain;

namespace CleanEventSourcing.Application.Interfaces
{
    public interface IEventStore
    {
        Task PublishEventsAsync(string stream, IEnumerable<IIntegrationEvent> events);

        Task<IEnumerable<IIntegrationEvent>> GetEvents(string stream);
    }
}