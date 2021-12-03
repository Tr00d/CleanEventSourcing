using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Domain;
using CleanEventSourcing.Domain.Items;
using Dawn;
using MediatR;

namespace CleanEventSourcing.Application.Items.CreateItem
{
    public class CreateItemHandler : IRequestHandler<CreateItemCommand>
    {
        private readonly IEventStore eventStore;
        private readonly IMediator mediator;

        public CreateItemHandler(IEventStore eventStore, IMediator mediator)
        {
            this.eventStore = Guard.Argument(eventStore, nameof(eventStore)).NotNull().Value;
            this.mediator = Guard.Argument(mediator, nameof(mediator)).NotNull().Value;
        }

        public async Task<Unit> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            Item item = new(request.Id, request.Description);

            //await this.eventStore.PublishEventsAsync(item.GetStream(), item.GetIntegrationEvents());
            async void Action(IIntegrationEvent integrationEvent) =>
                await this.mediator.Publish(integrationEvent, cancellationToken);

            item.GetIntegrationEvents().IfNone(new List<IIntegrationEvent>()).ToList().ForEach(Action);
            return Unit.Value;
        }
    }
}