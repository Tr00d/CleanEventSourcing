using LanguageExt;
using static LanguageExt.Prelude;

namespace CleanEventSourcing.Domain.Items.Events
{
    public class ItemBaseEvent
    {
        public bool CanConvertTo<T>() where T : IAggregate => typeof(T) == typeof(Item);

        public Option<IIntegrationEvent<T>> ConvertTo<T>(IIntegrationEvent<Item> integrationEvent)
            where T : IAggregate =>
            this.CanConvertTo<T>() ? Some((IIntegrationEvent<T>) integrationEvent) : Option<IIntegrationEvent<T>>.None;
    }
}