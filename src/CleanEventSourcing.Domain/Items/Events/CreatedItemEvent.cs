using System;
using LanguageExt;

namespace CleanEventSourcing.Domain.Items.Events
{
    public class CreatedItemEvent : IIntegrationEvent<Item>
    {
        public CreatedItemEvent(Option<string> stream, Guid id, Option<string> description)
        {
            this.Stream = stream;
            this.Id = id;
            this.Description = description;
        }

        public Guid Id { get; set; }

        public Option<string> Description { get; set; }

        public void Apply(Option<Item> aggregate)
        {
            throw new NotImplementedException();
        }

        public Option<string> Stream { get; }

        public DateTime CreationDate { get; }

        public Type GetEventType()
        {
            throw new NotImplementedException();
        }

        public bool CanConvertTo<T>() where T : IAggregate
        {
            throw new NotImplementedException();
        }

        public Option<IIntegrationEvent<T>> TryConvertTo<T>() where T : IAggregate
        {
            throw new NotImplementedException();
        }
    }
}