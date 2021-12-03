using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
        
        public Task<Option<ItemSummary>> Handle(GetItemQuery request, CancellationToken cancellationToken) => throw new System.NotImplementedException();
    }
}