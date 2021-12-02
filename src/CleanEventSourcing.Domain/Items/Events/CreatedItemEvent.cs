using System;
using LanguageExt;

namespace CleanEventSourcing.Domain.Items.Events
{
    public class CreatedItemEvent : IIntegrationEvent<Item>
    {
        public CreatedItemEvent(Guid id, string description)
        {
            Id = id;
            Description = description;
        }


        public Guid Id { get; set; }

        public string Description { get; set; }

        public void Apply(Item aggregate)
        {
            throw new NotImplementedException();
        }

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