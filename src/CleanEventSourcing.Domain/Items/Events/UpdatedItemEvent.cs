using System;
using LanguageExt;

namespace CleanEventSourcing.Domain.Items.Events
{
    public class UpdatedItemEvent : IIntegrationEvent<Item>
    {
        public UpdatedItemEvent(Option<string> oldDescription, Option<string> newDescription)
        {
            this.OldDescription = oldDescription;
            this.NewDescription = newDescription;
        }
        
        public void Apply(Option<Item> aggregate)
        {
            throw new NotImplementedException();
        }

        public Option<string> OldDescription { get; }

        public Option<string> NewDescription { get; }

        public Option<string> Stream { get; set; }
        public bool CanConvertTo<T>() where T : IAggregate => throw new NotImplementedException();

        public Option<IIntegrationEvent<T>> ConvertTo<T>() where T : IAggregate => throw new NotImplementedException();
    }
}