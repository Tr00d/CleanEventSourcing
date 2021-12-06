using System;
using System.Collections.Generic;
using CleanEventSourcing.Domain.Items.Events;
using LanguageExt;

namespace CleanEventSourcing.Domain.Items
{
    public class Item : IAggregate
    {
        private readonly List<IIntegrationEvent<Item>> events;

        public Item()
        {
            this.events = new List<IIntegrationEvent<Item>>();
        }

        public Item(Guid id, Option<string> description)
            : this()
        {
            this.Id = id;
            this.Description = description;
            this.events.Add(new CreatedItemEvent(this.Id, this.Description));
        }

        public Option<string> Description { get; private set; }

        public Guid Id { get; private set; }

        public Option<IEnumerable<IIntegrationEvent>> GetIntegrationEvents() =>
            new List<IIntegrationEvent>(this.events);

        public void Update(Option<string> description)
        {
            Option<string> oldDescription = this.Description;
            this.Description = description;
            this.events.Add(new UpdatedItemEvent(this.Id, oldDescription, this.Description));
        }

        public void Apply(Option<CreatedItemEvent> integrationEvent) => integrationEvent.IfSome(this.Apply);

        private void Apply(CreatedItemEvent integrationEvent)
        {
            this.Id = integrationEvent.Id;
            this.Description = integrationEvent.Description;
        }

        public void Apply(Option<UpdatedItemEvent> integrationEvent) => integrationEvent.IfSome(this.Apply);

        private void Apply(UpdatedItemEvent integrationEvent)
        {
            this.Description = integrationEvent.NewDescription;
        }
    }
}