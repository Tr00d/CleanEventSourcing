using AutoMapper;
using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Application.Items.DeleteItem;
using CleanEventSourcing.Application.Items.GetItem;
using CleanEventSourcing.Application.Items.UpdateItem;
using Dawn;
using LanguageExt;
using MediatR;
using static LanguageExt.Prelude;

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
            => await request.Map(value => this.mapper.Map<CreateItemCommand>(value))
                .IfSomeAsync(value => this.mediator.Send(value))
                .ConfigureAwait(false);

        public async Task<Option<GetItemResponse>> GetAsync(Option<GetItemRequest> request) =>
            (await request
                .Map(value => this.mapper.Map<GetItemQuery>(value))
                .MapAsync(async query => await this.mediator.Send(query)))
            .Map(summary => this.mapper.Map<GetItemResponse>(summary));

        public async Task UpdateAsync(Option<UpdateItemRouteRequest> routeRequest,
            Option<UpdateItemBodyRequest> bodyRequest) =>
            await (from route in routeRequest
                    from body in bodyRequest
                    select Tuple(route, body))
                .Map(request => this.mapper.Map<UpdateItemCommand>(request))
                .IfSomeAsync(async command => await this.mediator.Send(command));

        public async Task DeleteAsync(Option<DeleteItemRequest> request) =>
            await request
                .Map(value => this.mapper.Map<DeleteItemCommand>(value))
                .IfSomeAsync(command => this.mediator.Send(command));
    }
}