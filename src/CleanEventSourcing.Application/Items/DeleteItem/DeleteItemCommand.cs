using System;
using MediatR;

namespace CleanEventSourcing.Application.Items.DeleteItem
{
    public class DeleteItemCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}