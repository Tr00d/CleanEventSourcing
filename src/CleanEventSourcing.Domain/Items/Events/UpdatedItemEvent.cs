using System;
using LanguageExt;
using static LanguageExt.Prelude;

namespace CleanEventSourcing.Domain.Items.Events
{
    public class UpdatedItemEvent : IIntegrationEvent<Item>
    {
        private readonly ItemEventBase eventBase = new();
        
        public UpdatedItemEvent(Guid id, Option<string> oldDescription, Option<string> newDescription)
        {
            this.Id = id;
            this.OldDescription = oldDescription;
            this.NewDescription = newDescription;
        }

        public Option<string> OldDescription { get; }

        public Option<string> NewDescription { get; }

        public Guid Id { get; }

        public void Apply(Option<Item> aggregate) => aggregate.IfSome(value => value.Apply(this));
        public Option<string> Stream { get; set; }

        public bool CanConvertTo<T>() where T : IAggregate => this.eventBase.CanConvertTo<T>();

        public Option<IIntegrationEvent<T>> ConvertTo<T>() where T : IAggregate =>
            this.eventBase.ConvertTo((IIntegrationEvent<T>) this);
    }
}