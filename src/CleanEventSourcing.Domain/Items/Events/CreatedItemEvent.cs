using System;
using LanguageExt;

namespace CleanEventSourcing.Domain.Items.Events
{
    public class CreatedItemEvent : IIntegrationEvent<Item>
    {
        public CreatedItemEvent(Guid id, Option<string> description)
        {
            this.Id = id;
            this.Description = description;
        }

        public Guid Id { get; set; }

        public Option<string> Description { get; set; }

        public void Apply(Option<Item> aggregate)
        {
            throw new NotImplementedException();
        }

        public Option<string> Stream { get; set; }

        public bool CanConvertTo<T>() where T : IAggregate
        {
            throw new NotImplementedException();
        }

        public Option<IIntegrationEvent<T>> ConvertTo<T>() where T : IAggregate
        {
            throw new NotImplementedException();
        }
    }
}