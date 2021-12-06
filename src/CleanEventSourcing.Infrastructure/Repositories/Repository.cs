using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Domain;
using Dawn;
using LanguageExt;
using static LanguageExt.Prelude;
using MediatR;

namespace CleanEventSourcing.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : IAggregate, new()
    {
        private readonly IEventStore eventStore;
        private readonly IMediator mediator;

        public Repository(IEventStore eventStore, IMediator mediator)
        {
            this.eventStore = Guard.Argument(eventStore, nameof(eventStore)).NotNull().Value;
            this.mediator = Guard.Argument(mediator, nameof(mediator)).NotNull().Value;
        }

        public async Task SaveAsync(Option<T> aggregate)
        {
            IEnumerable<Task> tasks = aggregate
                .Map(value => value.GetIntegrationEvents().IfNone(Enumerable.Empty<IIntegrationEvent>()))
                .Map(events => events.Select(this.PublishEvent))
                .IfNone(Enumerable.Empty<Task>());
            await Task.WhenAll(tasks);
        }

        public async Task<Option<T>> GetAsync(Guid id)
        {
            return (await this.eventStore.GetEvents(this.GetStream(id)))
                .Map(list =>
                    list.Where(listItem => listItem.CanConvertTo<T>()).Select(listItem => listItem.ConvertTo<T>())
                        .ToList())
                .Map(this.CreateAggregate);
        }
        
        private T CreateAggregate(IEnumerable<Option<IIntegrationEvent<T>>> events)
        {
            T aggregate = new T();
            events.ToList().ForEach(listItem => listItem.IfSome(item => this.ApplyEvent(aggregate, Some(item))));
            return aggregate;
        }

        private async Task PublishEvent(IIntegrationEvent integrationEvent) =>
            await this.mediator.Publish(integrationEvent);

        private void ApplyEvent(T aggregate, Option<IIntegrationEvent<T>> integrationEvent) =>
            integrationEvent.IfSome(value => value.Apply(aggregate));

        private string GetStream(Guid id) => $"{typeof(T).Name}-{id}";
    }
}