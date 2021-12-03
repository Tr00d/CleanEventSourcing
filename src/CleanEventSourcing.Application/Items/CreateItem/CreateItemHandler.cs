using System.Threading;
using System.Threading.Tasks;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Domain.Items;
using Dawn;
using MediatR;
using static LanguageExt.Prelude;

namespace CleanEventSourcing.Application.Items.CreateItem
{
    public class CreateItemHandler : IRequestHandler<CreateItemCommand>
    {
        private readonly IEventStore eventStore;

        public CreateItemHandler(IEventStore eventStore)
        {
            this.eventStore = Guard.Argument(eventStore, nameof(eventStore)).NotNull().Value;
        }

        public async Task<Unit> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            Item item = new(request.Id, request.Description);
            await eventStore.PublishEventsAsync(item.GetStream(), Some(item.GetIntegrationEvents()));
            return Unit.Value;
        }
    }
}