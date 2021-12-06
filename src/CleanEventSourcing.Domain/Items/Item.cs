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

        public void Update(Option<string> description)
        {
            Option<string> oldDescription = this.Description;
            this.Description = description;
            this.events.Add(new UpdatedItemEvent(oldDescription, this.Description));
        }
        
        public Option<string> Description { get; private set; }

        public Guid Id { get; }

        public Option<IEnumerable<IIntegrationEvent>> GetIntegrationEvents() =>
            new List<IIntegrationEvent>(this.events);
    }
}