using CleanEventSourcing.Domain;
using LanguageExt;

namespace CleanEventSourcing.Application.Interfaces
{
    public interface IEventStore
    {
        Task<Option<IEnumerable<IIntegrationEvent>>> GetEvents(Option<string> stream);
    }
}