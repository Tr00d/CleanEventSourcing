using System.Threading.Tasks;
using AutoMapper;
using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Application.Items.GetItem;
using CleanEventSourcing.Domain.Items;
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
            => await request.IfSomeAsync(value => this.mediator.Send(this.mapper.Map<CreateItemCommand>(value)))
                .ConfigureAwait(false);

        public async Task<Option<GetItemResponse>> GetAsync(Option<GetItemRequest> request) =>
            (await request
                .Map(value => this.mapper.Map<GetItemQuery>(value))
                .MapAsync(async query => await this.mediator.Send(query))
                .IfNone(Option<ItemSummary>.None))
            .Map(summary => this.mapper.Map<GetItemResponse>(summary));
    }
}