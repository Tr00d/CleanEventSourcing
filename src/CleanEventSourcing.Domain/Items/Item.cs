using System;
using System.Collections.Generic;
using CleanEventSourcing.Domain.Items.Events;
using LanguageExt;

namespace CleanEventSourcing.Domain.Items
{
    public class Item : IAggregate
    {
        private readonly List<IIntegrationEvent<Item>> events;

        private Item()
        {
            this.events = new List<IIntegrationEvent<Item>>();
        }

        public Item(Guid id, Option<string> description)
            : this()
        {
            this.Id = id;
            this.Description = description;
            this.events.Add(new CreatedItemEvent(this.GetStream(), this.Id, this.Description));
        }

        public Option<string> Description { get; }

        public Guid Id { get; }

        public Option<string> GetStream() => $"{nameof(Item)}-{this.Id}";

        public Option<IEnumerable<IIntegrationEvent>> GetIntegrationEvents() =>
            new List<IIntegrationEvent>(this.events);
    }
}