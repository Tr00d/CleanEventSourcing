using CleanEventSourcing.Domain.Items.Events;
using LanguageExt;

namespace CleanEventSourcing.Domain.Items
{
    public class Item : IAggregate
    {
        private readonly AggregateBase baseAggregate = new();

        public Item()
        {
        }

        public Item(Guid id, Option<string> description)
            : this()
        {
            this.baseAggregate.Id = id;
            this.Description = description;
            this.baseAggregate.AddEvent(new CreatedItemEvent(this.Id, this.Description));
        }

        public Option<string> Description { get; private set; }

        public bool IsDeleted { get; private set; }

        public Guid Id => this.baseAggregate.Id;

        public Option<IEnumerable<IIntegrationEvent>> GetIntegrationEvents() =>
            new List<IIntegrationEvent>(this.baseAggregate.GetEvents());

        public void Update(Option<string> description)
        {
            var oldDescription = this.Description;
            this.Description = description;
            this.baseAggregate.AddEvent(new UpdatedItemEvent(this.Id, oldDescription, this.Description));
        }

        public void Delete()
        {
            this.IsDeleted = true;
            this.baseAggregate.AddEvent(new DeletedItemEvent(this.Id));
        }

        public void Apply(Option<CreatedItemEvent> integrationEvent) => integrationEvent.IfSome(this.Apply);

        private void Apply(CreatedItemEvent integrationEvent)
        {
            this.baseAggregate.Id = integrationEvent.Id;
            this.Description = integrationEvent.Description;
        }

        public void Apply(Option<UpdatedItemEvent> integrationEvent) =>
            integrationEvent.IfSome(value => this.Description = value.NewDescription);

        public void Apply(Option<DeletedItemEvent> integrationEvent) =>
            integrationEvent.IfSomeAsync(value => this.IsDeleted = true);
    }
}