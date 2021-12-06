using System;
using LanguageExt;

namespace CleanEventSourcing.Domain.Items.Events
{
    public class CreatedItemEvent : IIntegrationEvent<Item>
    {
        private readonly ItemEventBase eventBase = new();

        public CreatedItemEvent(Guid id, Option<string> description)
        {
            this.Id = id;
            this.Description = description;
        }

        public Option<string> Description { get; }

        public Guid Id { get; }

        public void Apply(Option<Item> aggregate) => aggregate.IfSome(value => value.Apply(this));

        public Option<string> Stream { get; set; }

        public bool CanConvertTo<T>() where T : IAggregate => this.eventBase.CanConvertTo<T>();

        public Option<IIntegrationEvent<T>> ConvertTo<T>() where T : IAggregate =>
            this.eventBase.ConvertTo((IIntegrationEvent<T>) this);
    }
}