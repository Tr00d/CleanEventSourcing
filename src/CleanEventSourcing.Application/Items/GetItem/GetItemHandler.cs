using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Domain.Items;
using Dawn;
using LanguageExt;
using MediatR;

namespace CleanEventSourcing.Application.Items.GetItem
{
    public class GetItemHandler : IRequestHandler<GetItemQuery, Option<ItemSummary>>
    {
        private readonly IReadService readService;

        public GetItemHandler(IReadService readService)
        {
            this.readService = Guard.Argument(readService, nameof(readService)).NotNull().Value;
        }

        public async Task<Option<ItemSummary>> Handle(GetItemQuery request, CancellationToken cancellationToken) =>
            await this.readService.GetItemAsync(request.Id).ConfigureAwait(false);
    }
}