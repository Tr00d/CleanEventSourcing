using LanguageExt;

namespace CleanEventSourcing.Domain
{
    public interface IAggregate
    {
        Guid Id { get; }

        Option<IEnumerable<IIntegrationEvent>> GetIntegrationEvents();
    }
}