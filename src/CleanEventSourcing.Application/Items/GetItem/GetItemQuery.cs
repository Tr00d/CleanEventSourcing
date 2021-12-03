using System;
using CleanEventSourcing.Domain.Items;
using LanguageExt;
using MediatR;

namespace CleanEventSourcing.Application.Items.GetItem
{
    public class GetItemQuery : IRequest<Option<ItemSummary>>
    {
        public Guid Id { get; set; }
    }
}