using System;
using LanguageExt;
using static LanguageExt.Prelude;

namespace CleanEventSourcing.Domain.Items.Events
{
    public class UpdatedItemEvent : IIntegrationEvent<Item>
    {
        public UpdatedItemEvent(Guid id, Option<string> oldDescription, Option<string> newDescription)
        {
            this.Id = id;
            this.OldDescription = oldDescription;
            this.NewDescription = newDescription;
        }

        public Option<string> OldDescription { get; }

        public Option<string> NewDescription { get; }

        public void Apply(Option<Item> aggregate) => aggregate.IfSome(value => value.Apply(this));

        public Guid Id { get; }
        public Option<string> Stream { get; set; }

        public bool CanConvertTo<T>() where T : IAggregate => typeof(T) == typeof(Item);

        public Option<IIntegrationEvent<T>> ConvertTo<T>() where T : IAggregate =>
            this.CanConvertTo<T>() ? Some((IIntegrationEvent<T>) this) : Option<IIntegrationEvent<T>>.None;
    }
}