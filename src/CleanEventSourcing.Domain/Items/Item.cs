using System;
using System.Collections.Generic;
using CleanEventSourcing.Domain.Items.Events;

namespace CleanEventSourcing.Domain.Items
{
    public class Item : IAggregate
    {
        private readonly List<IIntegrationEvent<Item>> events;

        private Item()
        {
            events = new List<IIntegrationEvent<Item>>();
        }

        public Item(Guid id, string description)
            : this()
        {
            Id = id;
            Description = description;
            events.Add(new CreatedItemEvent(Id, Description));
        }

        public string Description { get; }

        public Guid Id { get; }

        public string GetStream() => $"{nameof(Item)}-{Id}";

        public IEnumerable<IIntegrationEvent> GetIntegrationEvents() => new List<IIntegrationEvent>(events);
    }
}