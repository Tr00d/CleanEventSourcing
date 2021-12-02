using System.Threading.Tasks;
using AutoMapper;
using CleanEventSourcing.Application.Items.CreateItem;
using Dawn;
using LanguageExt;
using MediatR;

namespace CleanEventSourcing.Application.Items
{
    public class ItemService : IItemService
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public ItemService(IMediator mediator, IMapper mapper)
        {
            this.mediator = Guard.Argument(mediator, nameof(mediator)).NotNull().Value;
            this.mapper = Guard.Argument(mapper, nameof(mapper)).NotNull().Value;
        }

        public async Task CreateAsync(Option<CreateItemRequest> request)
            => await request.IfSomeAsync(value => mediator.Send(mapper.Map<CreateItemCommand>(value)))
                .ConfigureAwait(false);
    }
}