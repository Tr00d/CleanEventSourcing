using System;
using LanguageExt;

namespace CleanEventSourcing.Domain.Items.Events
{
    public class DeletedItemEvent : IIntegrationEvent<Item>
    {
        public DeletedItemEvent(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }

        public void Apply(Option<Item> aggregate)
        {
            throw new NotImplementedException();
        }

        public Option<string> Stream { get; set; }
        public bool CanConvertTo<T>() where T : IAggregate => throw new NotImplementedException();

        public Option<IIntegrationEvent<T>> ConvertTo<T>() where T : IAggregate => throw new NotImplementedException();
    }
}