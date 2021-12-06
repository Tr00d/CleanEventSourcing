using LanguageExt;
using static LanguageExt.Prelude;

namespace CleanEventSourcing.Domain.Items.Events
{
    public class ItemEventBase
    {
        public bool CanConvertTo<T>() where T : IAggregate => typeof(T) == typeof(Item);

        public Option<IIntegrationEvent<T>> ConvertTo<T>(IIntegrationEvent<T> integrationEvent) where T : IAggregate =>
            this.CanConvertTo<T>() ? Some(integrationEvent) : Option<IIntegrationEvent<T>>.None;
    }
}