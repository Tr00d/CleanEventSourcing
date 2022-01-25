using LanguageExt;

namespace CleanEventSourcing.Domain.Items.Events
{
    public class UpdatedItemEvent : IIntegrationEvent<Item>
    {
        private readonly ItemBaseEvent baseEvent = new();

        public UpdatedItemEvent(Guid id, Option<string> oldDescription, Option<string> newDescription)
        {
            this.Id = id;
            this.OldDescription = oldDescription;
            this.NewDescription = newDescription;
        }

        public Option<string> OldDescription { get; }

        public Option<string> NewDescription { get; }

        public Guid Id { get; }

        public void Apply(Option<Item> aggregate) => aggregate.IfSome(value => value.Apply(this));
        public Option<string> Stream { get; set; }

        public bool CanConvertTo<T>() where T : IAggregate => this.baseEvent.CanConvertTo<T>();

        public Option<IIntegrationEvent<T>> ConvertTo<T>() where T : IAggregate => this.baseEvent.ConvertTo<T>(this);
    }
}