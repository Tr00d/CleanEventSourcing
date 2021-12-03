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
            events = new List<IIntegrationEvent<Item>>();
        }

        public Item(Guid id, Option<string> description)
            : this()
        {
            Id = id;
            Description = description;
            events.Add(new CreatedItemEvent(Id, Description));
        }

        public Option<string> Description { get; }

        public Guid Id { get; }

        public Option<string> GetStream() => $"{nameof(Item)}-{Id}";

        public Option<IEnumerable<IIntegrationEvent>> GetIntegrationEvents() => new List<IIntegrationEvent>(events);
    }
}